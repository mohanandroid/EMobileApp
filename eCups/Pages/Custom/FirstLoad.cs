using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Layouts.Custom;
using eCups.Renderers;
using MagicGradients;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class FirstLoad : Page
    {
        StackLayout ContentContainer;

        ColourButton SignUpButton;
        ColourButton SignInButton;

        StaticImage MainLogo;
        StaticLabel Info;
        StaticLabel AlreadyAMember;
        StaticLabel SignInLabel;
        StaticImage CupAndCrab;

        public FirstLoad()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.FirstLoad;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);
            AddDecor("bottom_decor.png", Units.ScreenHeight40Percent, LayoutOptions.EndAndExpand);

            ContentContainer = BuildContent();

            PageContent.Children.Add(ContentContainer, 0, 0);
        }

        public StackLayout BuildContent()
        {
            // build labels
            StackLayout mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight
            };


            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, 0);

            Info = new StaticLabel("Welcome to the first loyalty Ecup App. You are on the right step to saving our environment and oceans from plastic waste.");
            Info.Content.FontFamily = Fonts.GetRegularFont();
            Info.Content.FontSize = Units.FontSizeL;
            Info.Content.TextColor = Color.White;
            Info.CenterAlign();
            Info.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            Info.Content.VerticalTextAlignment = TextAlignment.Start;
            Info.Content.Margin = new Thickness(Units.ScreenHeight5Percent, Units.ScreenHeight5Percent, Units.ScreenHeight5Percent, 0);

            AlreadyAMember = new StaticLabel("Already an Ecups Member?");
            AlreadyAMember.Content.FontFamily = Fonts.GetRegularFont();
            AlreadyAMember.Content.FontSize = Units.FontSizeL;
            AlreadyAMember.Content.TextColor = Color.White;
            AlreadyAMember.CenterAlign();
            AlreadyAMember.Content.VerticalOptions = LayoutOptions.EndAndExpand;
            AlreadyAMember.Content.VerticalTextAlignment = TextAlignment.End;

            AlreadyAMember.Content.GestureRecognizers.Add(
               new TapGestureRecognizer()
               {
                   Command = new Command(() =>
                   {
                       Device.BeginInvokeOnMainThread(async () =>
                       {
                           await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Welcome);
                       });
                   })
               }
           );


            SignInLabel = new StaticLabel("Sign In");

            SignInLabel.Content.FontFamily = Fonts.GetRegularFont();
            SignInLabel.Content.FontSize = Units.FontSizeL;
            SignInLabel.Content.TextColor = Color.White;
            SignInLabel.Content.TextDecorations = TextDecorations.Underline;
            SignInLabel.CenterAlign();
            SignInLabel.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            SignInLabel.Content.VerticalTextAlignment = TextAlignment.Start;

            SignInLabel.Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Welcome);
                           });
                       })
                   }
               );


            CupAndCrab = new StaticImage("cup_and_crab.png", Units.ScreenWidth, null);
            CupAndCrab.Content.HeightRequest = Units.ScreenHeight35Percent;
            CupAndCrab.Content.Margin = new Thickness(0, Units.ScreenHeight5Percent, 0, 0);


            //SignUpButton = new ColourButton(Color.Transparent, Color.White, "Sign Up", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.SignUp));
            SignUpButton = new ColourButton(Color.Transparent, Color.White, "Sign Up", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.SignUp));

            SignUpButton.AddBorder(Color.White, 2);
            SignUpButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;


            //SignInButton = new ColourButton(Color.Blue, Color.White, "Sign In", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.WelcomeLogin));


            mainLayout.Children.Add(MainLogo.Content);
            mainLayout.Children.Add(Info.Content);
            mainLayout.Children.Add(CupAndCrab.Content);

            mainLayout.Children.Add(SignUpButton.Content);
            mainLayout.Children.Add(AlreadyAMember.Content);
            mainLayout.Children.Add(SignInLabel.Content);

            //mainLayout.Children.Add(SignInButton.Content);

            return mainLayout;
        }

        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);

                //this.HasHeader = false;
                //this.HasFooter = true;
                await base.Update();

                PageContent.Children.Remove(ContentContainer);
                ContentContainer = BuildContent();
                PageContent.Children.Add(ContentContainer);
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }
    }
}
