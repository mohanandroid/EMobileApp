using System;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.e.Tiles
{
    public class Tile : ActiveComponent
    {
        public Tile()
        {
            this.Content = new Grid
            {
                BackgroundColor = Color.LightGray,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 1
            };
        }

        public void AddContent(View content)
        {
            this.Content.Children.Add(content);
        }

    }
}
