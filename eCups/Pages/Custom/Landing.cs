using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Layouts.Custom;
using eCups.Models;
using eCups.Services;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class Landing : Page
    {
        StackLayout ContentContainer;

        public Landing()
        {
            this.IsScrollable = false;
            this.IsRefreshable = true;
            this.HasHeader = true;
            this.HasSubHeader = false;
            this.HasNavHeader = false;
            this.HasFooter = false;

            this.Id = (int)AppSettings.PageNames.Landing;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            Grid bg = new Grid
            {
                BackgroundColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight
            };

            // add a background?
            AddBackgroundView(bg);

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
        }

        public StackLayout BuildContent()
        {
            // build labels
            StackLayout mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight
            };

            Image image = new Image
            {
                Source = "splash_image.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            mainLayout.Children.Add(image);

            return mainLayout;
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
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }
    }
}