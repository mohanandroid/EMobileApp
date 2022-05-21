using System;
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
    public class CodeScanned : Page
    {
        StackLayout ContentContainer;

        StackLayout MainLayout;

        ColourButton ViewProfileButton;

        StaticImage MainLogo;
        StaticLabel CongratulationsLabel;
        StaticLabel CodeScannedLabel;
        StaticLabel Info;

        Grid CupAndAvatarLayout;
        Grid NumberedCupLayout;

        StaticImage CupImage;
        StaticImage AvatarImage;
        StaticLabel PointsLabel;

        public CodeScanned()
        {
            this.IsScrollable = true;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.CodeScanned;
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

            CongratulationsLabel = new StaticLabel("Congratulations");
            CongratulationsLabel.Content.FontFamily = Fonts.GetBoldFont();
            CongratulationsLabel.Content.FontSize = 32;
            CongratulationsLabel.Content.TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN);
            CongratulationsLabel.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            CongratulationsLabel.Content.VerticalTextAlignment = TextAlignment.Start;
            CongratulationsLabel.CenterAlign();

            CodeScannedLabel = new StaticLabel("QR Code Scanned");
            CodeScannedLabel.Content.FontFamily = Fonts.GetBoldFont();
            CodeScannedLabel.Content.FontSize = 24;
            CodeScannedLabel.Content.TextColor = Color.White;
            CodeScannedLabel.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            CodeScannedLabel.Content.VerticalTextAlignment = TextAlignment.Start;
            
            CodeScannedLabel.CenterAlign();

            Info = new StaticLabel("You have scanned the QR code and doing your part to save the ocean");
            Info.Content.FontFamily = Fonts.GetRegularFont();
            Info.Content.FontSize = Units.FontSizeL;
            Info.Content.TextColor = Color.White;
            Info.CenterAlign();
            Info.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            Info.Content.VerticalTextAlignment = TextAlignment.Start;
            Info.Content.Margin = new Thickness(Units.ScreenWidth10Percent, 0, Units.ScreenWidth10Percent, 0);


            CupAndAvatarLayout = new Grid { HeightRequest = Units.ScreenHeight30Percent};
            NumberedCupLayout = new Grid { };

            CupImage = new StaticImage("unbranded_cup.png", 240, null);

            AvatarImage = new StaticImage("jellyfish.png", 240, null);
            AvatarImage.Content.HorizontalOptions = LayoutOptions.EndAndExpand;

            PointsLabel = new StaticLabel(""+AppSession.CurrentUser.RewardPoints);
            PointsLabel.Content.FontFamily = Fonts.GetBoldFont();
            PointsLabel.Content.FontSize = 52;
            PointsLabel.Content.Margin = new Thickness(0, 24, 0, 0);
            PointsLabel.Content.TextColor = Color.White;

            PointsLabel.CenterAlign();


            NumberedCupLayout.Children.Add(CupImage.Content, 0, 0);
            NumberedCupLayout.Children.Add(PointsLabel.Content, 0, 0);

            CupAndAvatarLayout.Children.Add(NumberedCupLayout, 0, 0);
            CupAndAvatarLayout.Children.Add(AvatarImage.Content, 0, 0);



            ViewProfileButton = new ColourButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "View Your Profile", null);
            ViewProfileButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;
            ViewProfileButton.Content.Margin = new Thickness(0, Units.ScreenHeight10Percent, 0, Units.ScreenHeight10Percent);


            ViewProfileButton.Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {

                               //ShowLoggedInView();
                               AppSession.CurrentUser.AddPoints(5);
                               PointsLabel.Content.Text = "" + AppSession.CurrentUser.RewardPoints;

                               if (AppSession.CurrentUser.RewardPoints > 999) // probably never get this many
                               {
                                   PointsLabel.Content.FontSize = 40;
                               }

                               if (AppSession.CurrentUser.RewardPoints > 9999) // proper caffine fiend mad lad
                               {
                                   PointsLabel.Content.FontSize = 32;
                               }

                               //await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Menu);
                               AddMenuBar();
                           });
                       })
                   }
               );


            ShowMainView();

            return MainLayout;
        }

        public void ShowMainView()
        {
            MainLayout.Children.Clear();
            MainLayout.Children.Add(MainLogo.Content);
            MainLayout.Children.Add(CongratulationsLabel.Content);
            MainLayout.Children.Add(CodeScannedLabel.Content);
            MainLayout.Children.Add(Info.Content);
            MainLayout.Children.Add(CupAndAvatarLayout);//
            MainLayout.Children.Add(ViewProfileButton.Content);

            MainLayout.Children.Add(new ScanHudButton().Content);
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


                PointsLabel.Content.Text = "" + AppSession.CurrentUser.RewardPoints;
            }
        }
    }
}
