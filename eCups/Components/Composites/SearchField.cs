using System;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Helpers.Custom;
using Xamarin.Forms;
using XFShapeView;

namespace eCups.e.Composites
{
    public class SearchField
    {
        public Grid Content { get; set; }
        public StackLayout Container { get; set; }

        public string PlaceHolder { get; set; }
        public bool IsFuzzy { get; set; }

        public ShapeView SearchBoxShape { get; set; }
        public StaticImage Icon { get; set; }
        public Entry TextInput { get; set; }


        public SearchField(string placeholder)
        {
            Content = new Grid
            {
               BackgroundColor = Color.Transparent,
               WidthRequest = Dimensions.SEARCH_INPUT_WIDTH,
            };

            Container = new StackLayout
            {
                BackgroundColor = Color.Transparent,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = 8
            };

            SearchBoxShape = new ShapeView
            {
                ShapeType = ShapeType.Box,
                HeightRequest = Dimensions.SEARCH_INPUT_HEIGHT,
                WidthRequest = Units.ScreenWidth,//Dimensions.SEARCH_INPUT_WIDTH,
                Color = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = Dimensions.SEARCH_INPUT_HEIGHT
            };

            TextInput = new Entry
            {
                Placeholder = "---- " + placeholder + " ----",
                PlaceholderColor = Color.LightGray,
                //WidthRequest = Dimensions.SEARCH_INPUT_WIDTH,
                HeightRequest = Dimensions.SEARCH_INPUT_HEIGHT,
                Margin = new Thickness(16, 0),
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            
            };

            Icon = new StaticImage("icon.png", Dimensions.SEARCH_INPUT_ICON_WIDTH, Dimensions.SEARCH_INPUT_ICON_WIDTH, null);




            //
            Container.Children.Add(Icon.Content);
            Container.Children.Add(TextInput);

            Content.Children.Add(SearchBoxShape, 0, 0);
            Content.Children.Add(Container, 0, 0);


        }
    }
}
