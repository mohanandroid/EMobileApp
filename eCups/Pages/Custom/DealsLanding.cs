using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.Components.Buttons;
using eCups.DebugData.Custom;
using eCups.e.Buttons;
using eCups.e.CafeListing;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Helpers.Custom;
using eCups.Layouts.Custom;
using eCups.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class DealsLanding : Page
    {
        StackLayout ContentContainer;
        CScrollView scroll;
        StaticImage MainLogo;
        Grid Heading;
        List<DealListing> dealQuery;

        public DealsLanding()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.DealsLanding;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            scroll = new CScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Content = ShowDeals(),
                Elastic = false,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight - Units.ScreenHeight25Percent,
                Margin = new Thickness(0, 0, 0, 0),

            };

            UpdateUserLocation();

            // add a background?
            AddBackgroundView(scroll);
            

            AddMenuBar();
            AddDecor("top_decor.png", Units.ScreenHeight40Percent, LayoutOptions.Start);

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
        }

        public override Task TransitionIn()
        {
            UpdateUserLocation();
            return base.TransitionIn();
        }

        StackLayout ShowDeals()
        {
            StackLayout Layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.White,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                Padding = new Thickness(0,100,0,100),
            };

            //TODO API call
            if (true)
            {
                if (FakeData.DebugDealList.Length == 0)
                {
                    //No Deals Found
                }
                else
                {
                    dealQuery = new List<DealListing>();

                    foreach (DealObject deal in FakeData.DebugDealList)
                    {
                        dealQuery.Add(new DealListing(deal));
                    }

                    //order by distance to user
                    List<DealListing> sortedQuery = dealQuery.OrderBy(o => o.minutesFrom).ToList();

                    bool altColour = false;
                    for (int i = 0; i < sortedQuery.Count; i++)
                    {
                        Layout.Children.Add(sortedQuery[i].ReturnItem(altColour));
                        altColour = !altColour;
                    }
                }
            }

            return Layout;
        }

        private async void UpdateUserLocation()
        {
            try
            {
                AppSettings.userLocation = await Geolocation.GetLastKnownLocationAsync();
            }
            catch
            {

            }
        }


        public StackLayout BuildContent()
        {
            // build labels
            StackLayout mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight20Percent
            };


            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth30Percent, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenWidth5Percent, Units.ScreenHeight5Percent, 0, 0);
            MainLogo.Content.HorizontalOptions = LayoutOptions.Center;

            Heading = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = Units.ScreenWidth - Units.ScreenWidth40Percent},
                    new ColumnDefinition(),
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = 40, },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
                Margin = new Thickness(20, 20, 20, 0)

            };

            Label Title = new Label
            {
                TextColor = Color.White,
                FontSize = 24,
                FontFamily = Fonts.GetBoldFont(),
                Text = "Deals near you"
            };

            Label ChangeLocations = new Label
            {
                TextColor = Color.FromHex("#41EAD4"),
                TextDecorations = TextDecorations.Underline,
                Text = "Change Location",
                VerticalOptions = LayoutOptions.End
            };

            ChangeLocations.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.ChangeLocation);
                           });
                       })
                   }
               );

            Label Subtitle = new Label
            {
                TextColor = Color.White,
                Text = "View all the deals available in your area just for Ecup customers.",
                FontSize = 12,
                FontFamily = Fonts.GetRegularFont()
            };

            Heading.Children.Add(Title, 0, 0);
            Heading.Children.Add(ChangeLocations, 1, 0);
            Heading.Children.Add(Subtitle, 0, 1);

            mainLayout.Children.Add(MainLogo.Content);
            mainLayout.Children.Add(Heading);

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

                AddMenuBar();
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }
    }
}