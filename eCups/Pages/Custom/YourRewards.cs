using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.Components.Buttons;
using eCups.DebugData.Custom;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Helpers.Custom;
using eCups.Layouts.Custom;
using eCups.Models.Custom;
using MagicGradients;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class YourRewards : Page
    {
        CScrollView ContentContainer;

        public YourRewards()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.YourRewards;
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

        public CScrollView BuildContent()
        {
            Grid mainLayout = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                RowSpacing = 15,
                Margin = new Thickness(0, Units.ScreenHeight5Percent, 0, 0),
                Padding = new Thickness(0, 0, 0, 50),
                WidthRequest = Units.ScreenWidth,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                }
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

            Label greeting = new Label
            {
                Text = string.Format("{0}, {1}", AppSession.ReturnTimeOfDay(), AppSession.CurrentUser.FirstName),
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 24,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label usage = new Label
            {
                Text = "Your Plastic Cup Saving",
                FontFamily = Fonts.GetBoldFont(),
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
                HeightRequest = 215,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label cupText = new Label
            {
                Text = AppSession.CurrentUser.RewardPoints.ToString(),
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 48,
                TextColor = Color.White,
                Margin = new Thickness(0, 10, 0, -10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End
            };

            Grid loyaltyGrid = CreateLoyaltyGrid();

            loyaltyGrid.TranslateTo(0, -60, 0, null);

            Label loyaltyKey = new Label
            {
                Text = "Loyalty Meter Key",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_MID_GREEN),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            };

            StackLayout loyaltyMeterStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 30,
                BackgroundColor = Color.FromHex(Colors.EC_SUPER_LIGHT_GREY),
                WidthRequest = Units.ScreenWidth,
                //HeightRequest = 700,
                Padding = new Thickness(Units.ScreenWidth5Percent, 150, Units.ScreenWidth5Percent, 50)
            };

            loyaltyMeterStack.Children.Add(loyaltyKey);

            AddLoyaltyTiers(loyaltyMeterStack);

            loyaltyMeterStack.TranslateTo(0, -175, 0, null);

            StackLayout helpingStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                WidthRequest = Units.ScreenWidth,
                Spacing = 25,
                Margin = new Thickness(0, -200, 0, -100),
                Padding = new Thickness(Units.ScreenWidth5Percent, 35, Units.ScreenWidth5Percent, 150)
            };

            Label helpingHeader = new Label
            {
                Text = "How you are Helping the Environment",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_MID_GREEN),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            };

            Label helpingBody = new Label
            {
                Text = FakeData.LoremIpsum(),
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 12,
                TextColor = Color.FromHex(Colors.EC_MID_GREEN),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            };

            helpingStack.Children.Add(helpingHeader);
            helpingStack.Children.Add(helpingBody);

            mainLayout.Children.Add(header, 0, 0);
            mainLayout.Children.Add(greeting, 0, 1);
            mainLayout.Children.Add(usage, 0, 2);
            mainLayout.Children.Add(cup,0, 3);
            mainLayout.Children.Add(cupText, 0, 3);
            mainLayout.Children.Add(helpingStack, 0, 6);
            mainLayout.Children.Add(loyaltyMeterStack, 0, 5);
            mainLayout.Children.Add(loyaltyGrid, 0, 4);

            AddMenuBar();
            return mainView;
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
                AddMenuBar();
                App.HideMenuButton();
            }
        }

        async void AddLoyaltyTiers(StackLayout parent)
        {
            //TODO add api
            if (true)
            {
                if (FakeData.LoyaltyTierList.Length == 0)
                {
                    //No loyalty levels found
                }
                else
                {
                    foreach (LoyaltyTierObject loyalty in FakeData.LoyaltyTierList)
                    {
                        LoyaltyListing listing = new LoyaltyListing(loyalty);
                        parent.Children.Add(listing.Content);
                    }
                }
            }
        }

        Grid CreateLoyaltyGrid()
        {
            Grid statusGrid = new Grid
            {
                BackgroundColor = Color.Transparent,
                HeightRequest = 450,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };

            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) });
            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            statusGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            statusGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            statusGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Image decor = new Image
            {
                Source = "middleDecoration.png",
                Margin = new Thickness(-Units.ScreenWidth10Percent, -Units.ScreenHeight10Percent),
                Aspect = Aspect.AspectFit,
                MinimumWidthRequest = Units.ScreenWidth,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Label loyalty = new Label
            {
                Text = "Your Loyalty Meter",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.White,
                Margin = new Thickness(Units.ScreenWidth5Percent, 0, -Units.ScreenWidth5Percent, 0),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            };

            statusGrid.Children.Add(decor, 0, 0);
            Grid.SetColumnSpan(decor, statusGrid.ColumnDefinitions.Count);
            Grid.SetRowSpan(decor, statusGrid.RowDefinitions.Count);

            Image loyaltyLevelIcon = new Image
            {
                Source = "starfish.png",
                Aspect = Aspect.AspectFit,
                HeightRequest = 140,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout LoyaltyStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 10,
                BackgroundColor = Color.Transparent,
                WidthRequest = Units.ScreenWidth,
                HeightRequest = 140,
            };

            Label loyaltyHeader = new Label
            {
                Text = "Starfish - Level 1",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label congrats = new Label
            {
                Text = "Well done on getting your star fish loyalty.",
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 12,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label rewardBreakdown = new Label
            {
                Text = "This level gives you 5% off your next order",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 12,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            LoyaltyStack.Children.Add(loyaltyHeader);
            LoyaltyStack.Children.Add(congrats);
            LoyaltyStack.Children.Add(rewardBreakdown);

            statusGrid.Children.Add(loyalty, 0, 1);
            statusGrid.Children.Add(loyaltyLevelIcon, 0, 2);
            statusGrid.Children.Add(LoyaltyStack, 1, 2);

            return statusGrid;
        }
    }
}