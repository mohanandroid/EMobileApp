using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.DatabaseObjects;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class CafePage : Page
    {
        StackLayout ContentContainer;
        Label CafeName;

        public CafePage()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.CafePage;
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

        public override Task TransitionIn()
        {
            SetContent(DebugData.Custom.FakeData.DebugCafeList[AppSettings.activeStore]);
            return base.TransitionIn();
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

            CafeName = new Label
            {
                Text = "NONE",
                FontSize = 40,
                TextColor = Color.Cyan,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };

            AddMenuBar();
            mainLayout.Children.Add(CafeName);
            mainLayout.Children.Add(new Label { Text = "Cafe Page" });
            return mainLayout;
        }

        private void SetContent(CafeObject cafe)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                CafeName.Text = cafe.Name;
                //Address.Text = cafe.Address;
                //Telephone.Text = cafe.Phone;
                //Website.Text = cafe.URL;
            });
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
                AddMenuBar();
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }


    }
}