using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Composites;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Helpers.Custom;
using eCups.Layouts;
using eCups.Layouts.Custom;
using eCups.Layouts.Custom.Tiles;
using eCups.Models.Custom;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class SettingsPage : Page
    {
        StackLayout ContentContainer;
        StackLayout UpdatableElementsContainer;
        int timesRefreshed;

        public SettingsPage()
        {
            this.IsScrollable = true;
            this.IsRefreshable = true;
            this.HasHeader = true;
            this.HasSubHeader = true;
            this.HasNavHeader = true;
            this.HasFooter = true;

            this.Id = (int)AppSettings.PageNames.AboutUs;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;
            timesRefreshed = 0;
            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);

            // customise
            if (this.RefreshView != null)
            {
                this.RefreshView.RefreshColor = Color.FromHex(Colors.BH_DARK_GREEN);
            }

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
            };

            Label TitleLabel = new Label
            {
                Text = "Settings",
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 24,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
            };

            mainLayout.Children.Add(TitleLabel);

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
                App.ShowMenuButton();
                //App.HideMenuButton();
            }
        }

        public override void ExecuteRefreshCommand()
        {
            //Device.InvokeOnMainThreadAsync(RefreshPage);

            RefreshPage();
        }

        private async void RefreshPage()
        {
            timesRefreshed++;
            await Task.Delay(1500);


            UpdatableElementsContainer.Children.Clear();

            //AppSession.TestItems = await App.ApiBridge.GetItems(AppSession.CurrentUser).ConfigureAwait(false);

            List<Item> items = AppSession.TestItems;

            foreach (Item item in items)
            {
                ItemLayout itemLayout = new ItemLayout(item);
                itemLayout.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
                itemLayout.MainImage.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;

                UpdatableElementsContainer.Children.Add(itemLayout.Content);
            }




            // Stop refreshing
            IsRefreshing = false;
            //base.RefreshView.IsRefreshing = false;
            RefreshView.IsRefreshing = false;
        }
    }
}