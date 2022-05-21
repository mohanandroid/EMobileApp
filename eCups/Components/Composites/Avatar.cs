using System;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.e.Composites
{
    public class Avatar : ActiveComponent
    {
        public StackLayout ContentContainer { get; set; }
        public ActiveImage Icon { get; set; }
        public ActiveLabel Title { get; set; }

        public Avatar(string title, string iconImageSource, int width, int height)
        {
            Content = new Grid
            {
                BackgroundColor = Color.Green,
                WidthRequest = width,
                HeightRequest = height,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            ContentContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Icon = new ActiveImage(iconImageSource, width, height, null, null);

            Title = new ActiveLabel(title, Units.FontSizeM, FontName.LatoRegular, Color.Transparent, Color.White, null);
            Title.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;

            ContentContainer.Children.Add(Icon.Content);
            ContentContainer.Children.Add(Title.Content);

            Content.Children.Add(ContentContainer);

        }
    }
}
