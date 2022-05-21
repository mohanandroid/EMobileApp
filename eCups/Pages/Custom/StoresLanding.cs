using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;
using eCups.DatabaseObjects;
using eCups.e.CafeListing;
using eCups.DebugData.Custom;
using Xamarin.Essentials;
using eCups.e.Images;
using System.Collections.Generic;
using System.Linq;

namespace eCups.Pages.Custom
{
    public class StoresLanding : Page
    {
        StackLayout ContentContainer;
        StackLayout CafeList;
        ScrollView scroll;
        StaticImage MainLogo;
        Grid Heading;
        List<CafeListing> cafeQuery;

        public StoresLanding()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.StoresLanding;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            CafeList = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Transparent,
                WidthRequest = Units.ScreenWidth,
            };

            scroll = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Content = CafeList,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight - Units.ScreenHeight25Percent,
                Margin = new Thickness(0, 0, 0, 0),
            };

            UpdateUserLocation();

            bool altBg = false;

            if(FakeData.DebugCafeList.Length == 0)
            {
                //return "No cafes found"
            }
            else
            {
                //Add blank for top padding
                CafeList.Children.Add(new CafeListing(altBg).Content);

                cafeQuery = new List<CafeListing>();

                foreach (CafeObject cafe in FakeData.DebugCafeList) //get full query
                {
                    cafeQuery.Add(new CafeListing(cafe));
                }

                //order by distance to user
                List<CafeListing> sortedQuery = cafeQuery.OrderBy(o => o.minutesFrom).ToList();

                for (int i = 0; i < sortedQuery.Count; i++)
                {
                    CafeList.Children.Add(sortedQuery[i].ReturnItem(altBg)); //ReturnItem function used to keep alternating background pattern after sorting list
                    altBg = !altBg;
                }

                //Add blank for bottom padding
                CafeList.Children.Add(new CafeListing(!altBg).Content);

                // add a background?
                AddBackgroundView(scroll);
            }

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
                    new RowDefinition { Height = 40 }
                },
                Margin = new Thickness(20, 20, 20, 0)

            };

            Label Title = new Label
            {
                TextColor = Color.White,
                FontSize = 24,
                FontFamily = Fonts.GetBoldFont(),
                Text = "Stores near you"
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
                Text = "View all the stores available in your area, just for eCup customers",
                FontSize = 12,
                FontFamily = Fonts.GetRegularFont()
            };

            Heading.Children.Add(Title, 0, 0);
            Heading.Children.Add(ChangeLocations, 1, 0);
            Heading.Children.Add(Subtitle, 0, 1);

            /*mainLayout.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Landing);
                           });
                       })
                   }
               );*/

            mainLayout.Children.Add(MainLogo.Content);
            mainLayout.Children.Add(Heading);
            //mainLayout.Children.Add(new Label { Text = "Stores Landing" });
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
