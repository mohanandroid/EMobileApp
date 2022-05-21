using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class WelcomeLogin : Page
    {
        StackLayout ContentContainer;

        ColourButton LogInButton;

        public WelcomeLogin()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.WelcomeLogin;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
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
            /*
            mainLayout.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               // also the target of auto-login
                               await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Welcome);
                           });
                       })
                   }
               );
            */
            LogInButton = new ColourButton(Color.Blue, Color.White, "Login", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Welcome));

            mainLayout.Children.Add(LogInButton.Content);


            mainLayout.Children.Add(new Label { Text = "Welcome Login" });
            return mainLayout;
        }

        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);

                this.HasHeader = false;
                this.HasFooter = true;
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
