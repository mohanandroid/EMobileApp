using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.Components.Buttons;
using eCups.DebugData.Custom;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Helpers.Custom;
using eCups.Layouts.Custom;
using eCups.Models;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class Home : Page
    {
        ScrollView ContentContainer;

        ColourButton MenuButton;
        ColourButton ScannerButton;

        public Home()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.Home;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.White
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            //AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_START);


            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);

            AddMenuBar();
        }

        public ScrollView BuildContent()
        {

            StackLayout mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                Padding = new Thickness(0, Units.ScreenHeight5Percent, 0, 0),
                WidthRequest = Units.ScreenWidth,
                MinimumHeightRequest = Units.ScreenHeight
            };

            CScrollView mainView = new CScrollView
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                Elastic = false,
                Content = mainLayout
            };

            Image header = new Image
            {
                Source = "ecups_logo.png",
                Aspect = Aspect.AspectFit,
                HeightRequest = 15,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
                
            };

            Image decor = new Image
            {
                Source = "topDecoration.png",
                Margin = new Thickness(-Units.ScreenWidth20Percent, -Units.ScreenHeight15Percent),
                Aspect = Aspect.AspectFill,
                MinimumWidthRequest = Units.ScreenWidth,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand

            };

            Label greeting = new Label
            {
                Text =  string.Format("{0}, {1}", AppSession.ReturnTimeOfDay(),AppSession.CurrentUser.FirstName),
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 24,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label usage = new Label
            {
                Text = "Ecups Usage",
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label loyalty = new Label
            {
                Text = "Loyalty Meter",
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Image cup = new Image
            {
                Source = "blankCup.png",
                Aspect = Aspect.AspectFit,
                MinimumWidthRequest = Units.ScreenWidth,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand

            };

            Label cupText = new Label
            {
                Text = AppSession.CurrentUser.RewardPoints.ToString(),
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 32,
                TextColor = Color.White,
                Margin = new Thickness(0, 10, 0, -10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End
            };

            Image levelIcon = new Image
            {
                Source = "starfish.png",
                Aspect = Aspect.AspectFit,
                MinimumWidthRequest = Units.ScreenWidth,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand

            };

            Label saving = new Label
            {
                Text = "Plastic Cup Saving",
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 13,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label level = new Label
            {
                Text = "StarFish - Level 1",
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 13,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };



            Grid statusGrid = CreateStatusGrid();

            statusGrid.Children.Add(decor, 0, 0);
            Grid.SetColumnSpan(decor, statusGrid.ColumnDefinitions.Count);
            Grid.SetRowSpan(decor, statusGrid.RowDefinitions.Count);

            statusGrid.Children.Add(header, 0, 0);
            statusGrid.Children.Add(greeting, 0, 1);
            Grid.SetColumnSpan(header, statusGrid.ColumnDefinitions.Count);
            Grid.SetColumnSpan(greeting, statusGrid.ColumnDefinitions.Count);

            statusGrid.Children.Add(usage, 0, 2);
            statusGrid.Children.Add(loyalty, 1, 2);
            statusGrid.Children.Add(cup, 0, 3);
            statusGrid.Children.Add(cupText, 0, 3);
            statusGrid.Children.Add(levelIcon, 1, 3);
            statusGrid.Children.Add(saving, 0, 4);
            statusGrid.Children.Add(level, 1, 4);

            mainLayout.Children.Add(statusGrid);
            mainLayout.Children.Add(ShowDeals());


            return mainView;
        }

        StackLayout ShowDeals()
        {
            StackLayout Layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.White,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                Padding = new Thickness(0,0,0,50)
            };

            Label dealsHeader = new Label
            {
                Text = "Deals Recommended to you",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_MID_GREEN),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                Margin = new Thickness(Units.ScreenWidth5Percent,0,0,0)
            };

            Layout.Children.Add(dealsHeader);

            //TODO API call
            if (true)
            {
                if (FakeData.DebugDealList.Length == 0)
                {
                    //No Deals Found
                }
                else
                {

                    List<DealListing> dealQuery = new List<DealListing>();

                    foreach (DealObject deal in FakeData.DebugDealList)
                    {
                        dealQuery.Add(new DealListing(deal));
                    }

                    //order by distance to user
                    List<DealListing> sortedQuery = dealQuery.OrderBy(o => o.minutesFrom).ToList();

                    bool altColour = true;
                    for (int i = 0; i < sortedQuery.Count; i++)
                    {
                        Layout.Children.Add(sortedQuery[i].ReturnItem(altColour));
                        altColour = !altColour;
                    }
                }
            }

            return Layout;
        }

        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);

                this.HasHeader = false;
                this.HasFooter = false;
                await base.Update();

                PageContent.Children.Remove(ContentContainer);
                ContentContainer = BuildContent();
                PageContent.Children.Add(ContentContainer);
                //App.ShowMenuButton();
                AddMenuBar();
                App.HideMenuButton();
            }
        }

        Grid CreateStatusGrid()
        {
            Grid statusGrid = new Grid
            {
                BackgroundColor = Color.Transparent,
                HeightRequest = 350,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };

            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Absolute) });
            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute) });
            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Absolute) });
            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(115, GridUnitType.Absolute) });
            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Absolute) });

            statusGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            statusGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            return statusGrid;
        }
    }
}
