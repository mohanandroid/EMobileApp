using System;
using eCups.Branding;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.e.Fields
{
    public class SimpleInputField : ActiveComponent
    {

        public Label RequiredStar { get; set; }
        public CustomEntry TextEntry { get; set; }

        public SimpleInputField(string placeholder, Keyboard keyboard)
        {
            Content = new Grid
            {
                //HeightRequest = Units.InputHeight,
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(Units.ScreenUnitXS, 0)
            };

            TextEntry = new CustomEntry
            {
                FontFamily = eCups.Helpers.Fonts.GetRegularFont(),
                Placeholder = placeholder,
                PlaceholderColor = Color.Gray,
                TextColor = Color.Black,
                Keyboard = keyboard,
                FontSize = 24,
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.End,
                IsPassword = false
            };

            Content.Children.Add(TextEntry, 0, 0);
        }
    }
}
