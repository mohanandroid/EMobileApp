using System;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using eCups.Branding;
using eCups.Models.Custom;

namespace eCups.Components.Buttons
{
    public class LoyaltyListing : e.ActiveComponent
    {
        public Image LoyaltyIcon;
        public Label Title;
        public Label Para;

        public LoyaltyListing(LoyaltyTierObject loyalty)
        {
            Grid Container = new Grid
            {
                MinimumHeightRequest = 120,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 20,
                ColumnSpacing = 10,
                RowDefinitions =
                {
                    new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = new GridLength(70, GridUnitType.Absolute)},
                    new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star)}
                }
            };

            LoyaltyIcon = new Image
            {
                Source = loyalty.ImgUrl,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            string levelString = string.Format("{0} - Level {1}",loyalty.LevelName, loyalty.Level);

            Title = new Label
            {
                TextColor = Color.FromHex(Colors.EC_GREEN_3),
                FontSize = 12,
                Text = levelString,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.EndAndExpand,
                VerticalTextAlignment = TextAlignment.End,
                FontFamily = Fonts.GetBoldFont(),
                Margin = new Thickness(0, 10, 0, 0)
            };

            Para = new Label
            {
                TextColor = Color.FromHex(Colors.EC_GREEN_2),
                Text = loyalty.RewardBreakdown,
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.StartAndExpand,
                VerticalTextAlignment = TextAlignment.Start,
                FontFamily = Fonts.GetRegularFont(),
            };

            if (AppSettings.userLocation == null)
            {
                AppSettings.userLocation = new Xamarin.Essentials.Location(0, 0);
            }

            Container.Children.Add(LoyaltyIcon, 0, 0);
            Grid.SetRowSpan(LoyaltyIcon, Container.RowDefinitions.Count);
            Container.Children.Add(Title, 1, 0);
            Container.Children.Add(Para, 1, 1);
            Content.Children.Add(Container);
        }
    }
}
