using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Fields;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Layouts.Custom;
using eCups.Layouts.Custom.Tiles;
using eCups.Renderers;
using MagicGradients;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class Welcome : Page
    {
        StackLayout ContentContainer;

        StackLayout MainLayout;

        ColourButton MenuButton;
        ColourButton SignInButton;
        ColourButton BuyButton;
        ColourButton ViewRewardsButton;
        ColourButton AddReturnButton;

        StaticImage MainLogo;
        StaticLabel CongratulationsLabel;
        StaticLabel Info;

        StaticImage CupAndCrab;
        //StaticLabel AlreadyAMember;
        //StaticLabel MenuButton;
        //StaticImage CupAndCrab;
        BorderInputField Username;
        BorderInputField Password;

        ScanButton ScannerButton;

        public Welcome()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.Welcome;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);
            AddDecor("bottom_decor3.png", Units.ScreenHeight30Percent, LayoutOptions.EndAndExpand);

            ContentContainer = BuildContent();

            PageContent.Children.Add(ContentContainer, 0, 0);
        }

        public StackLayout BuildContent()
        {
            // build labels
            MainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight
            };

            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, 0);

            CongratulationsLabel = new StaticLabel("Welcome Back!");
            CongratulationsLabel.Content.FontFamily = Fonts.GetBoldFont();
            CongratulationsLabel.Content.FontSize = 32;
            CongratulationsLabel.Content.TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN);
            CongratulationsLabel.Content.HeightRequest = 48;
            CongratulationsLabel.Content.VerticalOptions = LayoutOptions.EndAndExpand;
            CongratulationsLabel.Content.VerticalTextAlignment = TextAlignment.End;
            CongratulationsLabel.CenterAlign();


            Info = new StaticLabel("Let's do our bit at saving the environment");
            Info.Content.FontFamily = Fonts.GetRegularFont();
            Info.Content.FontSize = Units.FontSizeL;
            Info.Content.TextColor = Color.White;
            Info.CenterAlign();
            Info.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            Info.Content.VerticalTextAlignment = TextAlignment.Start;
            Info.Content.Margin = new Thickness(Units.ScreenWidth10Percent, 0, Units.ScreenWidth10Percent, 0);

            ScannerButton = new ScanButton();
            ScannerButton.ScanButtonLayout.Margin = 0;

            ScannerButton.Content.WidthRequest = Units.LargeButtonWidth;
            ScannerButton.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;

            MenuButton = new ColourButton(Color.Transparent, Color.White, "Menu", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Menu));
            MenuButton.AddBorder(Color.White, 2);
            MenuButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;

            BuyButton = new ColourButton(Color.Transparent, Color.White, "Buy a Drink", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner));
            BuyButton.AddBorder(Color.White, 2);
            BuyButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;

            ViewRewardsButton = new ColourButton(Color.Transparent, Color.White, "View Rewards", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.YourRewards));
            ViewRewardsButton.AddBorder(Color.White, 2);
            ViewRewardsButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;

            AddReturnButton = new ColourButton(Color.Transparent, Color.White, "Add Cup / Return Cup", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner));
            AddReturnButton.AddBorder(Color.White, 2);
            AddReturnButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;

            SignInButton = new ColourButton(Color.Transparent, Color.FromHex(Colors.EC_BRIGHT_GREEN), "Login", null);
            SignInButton.AddBorder(Color.FromHex(Colors.EC_BRIGHT_GREEN), 2);
            SignInButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;

            SignInButton.Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               if (!string.IsNullOrEmpty(Username.GetText()) && !string.IsNullOrEmpty(Password.GetText()))
                               {
                                   await App.ShowLoading();
                                   LoginServiceCall();
                               }
                               else
                               {
                                   App.ShowAlert("Oh Dear!", "Please enter a valid detials");
                               }
                           });
                       })
                   }
               );

            CupAndCrab = new StaticImage("cup_and_crab.png", Units.ScreenWidth, null);
            CupAndCrab.Content.HeightRequest = Units.ScreenHeight25Percent;
            CupAndCrab.Content.Margin = new Thickness(0, Units.ScreenWidth5Percent, 0, Units.ScreenWidth10Percent);


            Username = new BorderInputField("Username", Keyboard.Email, true);
            Username.Content.Margin = new Thickness(Units.ScreenWidth5Percent, 0);
            Username.SetDarkBackgroundTheme();

            /*#if DEBUG
                        Username.TextEntry.Text = "Debug@test.co.uk";
            #endif*/

            Password = new BorderInputField("Password", Keyboard.Text, true);
            Password.Content.Margin = new Thickness(Units.ScreenWidth5Percent, 0);
            Password.SetDarkBackgroundTheme();
            Password.TextEntry.IsPassword = true;

            ShowLoggedOutView();

            return MainLayout;
        }



        public void ShowLoggedInView()
        {
            MainLayout.Children.Clear();
            MainLayout.Children.Add(MainLogo.Content);
            MainLayout.Children.Add(CongratulationsLabel.Content);
            MainLayout.Children.Add(Info.Content);

            MainLayout.Children.Add(MenuButton.Content);
            MainLayout.Children.Add(BuyButton.Content);
            MainLayout.Children.Add(ViewRewardsButton.Content);
            MainLayout.Children.Add(AddReturnButton.Content);
            //MainLayout.Children.Add(ScannerButton.Content);

            MainLayout.Children.Add(CupAndCrab.Content);
        }

        public void ShowLoggedOutView()
        {
            MainLayout.Children.Clear();
            MainLayout.Children.Add(MainLogo.Content);
            MainLayout.Children.Add(CongratulationsLabel.Content);
            MainLayout.Children.Add(Info.Content);

            MainLayout.Children.Add(Username.Content);
            MainLayout.Children.Add(Password.Content);
            MainLayout.Children.Add(SignInButton.Content);

            MainLayout.Children.Add(CupAndCrab.Content);
        }

        private async void LoginServiceCall()
        {
            var result = await App.ApiBridge.LogIn(Username.GetText(), Password.GetText());
            await App.HideLoading();
            if (result.error)
            {
                App.ShowAlert("Alert", "Login failed");
            }
            else
            {
                ShowLoggedInView();
               
            }

        }

        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);
                await base.Update();

                PageContent.Children.Remove(ContentContainer);
                ContentContainer = BuildContent();
                PageContent.Children.Add(ContentContainer);
                App.HideMenuButton();
            }
        }
    }
}
