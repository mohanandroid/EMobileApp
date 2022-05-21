using System;
using eCups.e.Images;
using Xamarin.Forms;

namespace eCups.e.Labels
{
    public class IconLabel
    {
        public Grid Content { get; set; }
        public StackLayout Container { get; set; }
        public StaticImage Icon { get; set; }
        public StaticLabel TextContent { get; set; }

        public IconLabel(string iconImageSource, string text, int width, int height)
        {
            Content = new Grid
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = width,
                HeightRequest = height
            };

            Container = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            Icon = new StaticImage("icon.png", height, height, null);

            TextContent = new StaticLabel(text);

            Container.Children.Add(Icon.Content);
            Container.Children.Add(TextContent.Content);

            Content.Children.Add(Container);


        }

        public void SetIconLeft()
        {
            Container.Children.Clear();
            Container.Children.Add(Icon.Content);
            Container.Children.Add(TextContent.Content);
        }

        public void SetIconRight()
        {
            Container.Children.Clear();
            Container.Children.Add(TextContent.Content);
            Container.Children.Add(Icon.Content);
        }
    }
}
