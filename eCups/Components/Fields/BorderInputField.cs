using System;
using Acr.UserDialogs;
using eCups.Branding;
using eCups.Helpers;
using Xamarin.Forms;
using XFShapeView;

namespace eCups.e.Fields
{
    public class BorderInputField : ActiveComponent
    {
        public Label RequiredStar { get; set; }
        public CustomEntry TextEntry { get; set; }
        public Frame BorderFrame { get; set; }
        public ShapeView BorderShape;

        public int FieldHeight;

        public BorderInputField(string placeholder, Keyboard keyboard, bool required)
        {
            
            FieldHeight = 48;

            Content = new Grid
            {
                WidthRequest = Units.ScreenWidth,
                HeightRequest = FieldHeight,
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            TextEntry = new CustomEntry
            {
                FontFamily = Fonts.GetRegularFont(),
                Placeholder = placeholder,
                PlaceholderColor = Color.Gray,
                TextColor = Color.White,
                Keyboard = keyboard, 
                FontSize = 16,
                WidthRequest = Units.ScreenWidth-32,
                HeightRequest = FieldHeight-6,
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsPassword = false,
                Margin = new Thickness(16, 0)
            };

            TextEntry.Focused += TextEntry_focused;

            BorderFrame = new Frame
            {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Black,
                HeightRequest = FieldHeight,
                CornerRadius = FieldHeight/2,
                HasShadow = false
            };

            BorderShape = new ShapeView
            {
                ShapeType = ShapeType.Box,
                HeightRequest = Units.TapSizeM,
                WidthRequest = Units.ScreenWidth,
                Color = Color.Transparent,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = FieldHeight / 2,
                BorderColor = Color.Black,
                BorderWidth = 2,
                Opacity = 1
            };


            if (required)
            {
                TextEntry.Placeholder = placeholder + "*";
            }

            //Content.Children.Add(BorderFrame, 0, 0);
            Content.Children.Add(BorderShape, 0, 0);

            Content.Children.Add(TextEntry, 0, 0);
        }

        private void TextEntry_focused(object sender, FocusEventArgs e)
        {
            Entry entry = sender as Entry;
            if(entry.Placeholder.Contains("Date of Birth"))
            {
                UserDialogs.Instance.DatePrompt(new DatePromptConfig
                {
                  MaximumDate = DateTime.Now,
                  iOSPickerStyle = iOSPickerStyle.Wheels,
                  OnAction = (result) => SetDateOfBirth(result),
                  IsCancellable = true
                });
                
            }
        }

        private void SetDateOfBirth(DatePromptResult result)
        {
            if (result.Ok)
            {
                TextEntry.Text = result.SelectedDate.ToString("yyyy-MM-dd");
            }
        }

        public string GetText()
        {
            return TextEntry.Text;
        }

        public void SetThemeColors(Color backgroundColor, Color borderColor, Color placeHolderColor, Color textColour)
        {
            TextEntry.TextColor = textColour;
            TextEntry.PlaceholderColor = placeHolderColor;

            BorderFrame.BorderColor = borderColor;
            BorderFrame.BackgroundColor = backgroundColor;
            TextEntry.BackgroundColor = backgroundColor;
            Content.BackgroundColor = backgroundColor;

            BorderShape.BorderColor = borderColor;
        }

        public void SetLightBackgroundTheme()
        {
            SetThemeColors(Color.Transparent, Color.FromHex(Colors.EC_GREEN_2), Color.Gray, Color.Black);
        }

        public void SetDarkBackgroundTheme()
        {
            SetThemeColors(Color.Transparent, Color.White, Color.White, Color.White);
        }
    }
}
