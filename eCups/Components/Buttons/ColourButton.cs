using System;
using eCups.Helpers;
using eCups.Services.Storage;
using eCups.Tools;
using Xamarin.Forms;
using XFShapeView;

namespace eCups.e.Buttons
{
    public class ColourButton : e.ActiveComponent
    {
        public Grid Button;
        public Label Label;
        public ShapeView ButtonShape;
        public ShapeView DropShadow;

        public ColourButton(Color backgroundColor, Color textColor, string buttonText, Models.Action action)
        {
            this.DefaultAction = action;

            this.Content = new Grid
            {
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.Center
            };

            ButtonShape = new ShapeView
            {
                ShapeType = ShapeType.Box,
                HeightRequest = Units.TapSizeM,
                WidthRequest = Units.LargeButtonWidth,
                Color = backgroundColor,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = Units.TapSizeM/2,
                Opacity = 1
            };

            DropShadow = new ShapeView
            {
                ShapeType = ShapeType.Box,
                HeightRequest = Units.TapSizeM,
                WidthRequest = Units.LargeButtonWidth,
                Color = Color.Gray,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = Units.TapSizeM / 2,
                Opacity = 0.5
            };


            Button = new Grid
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = ButtonShape.Width,
                HeightRequest = ButtonShape.Height,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Label = new Label
            {
                TextColor = textColor,
                Text = buttonText,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = Fonts.GetBoldFont(),
            };

            if (this.DefaultAction != null)
            {
                this.Content.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                if(buttonText== "Add Cup / Return Cup")
                                {
                                    LocalDataStore.Save("Page", "Add Cup / Return Cup");
                                }else if(buttonText == "Buy a Drink")
                                {
                                    LocalDataStore.Clear("Qrcode");
                                    LocalDataStore.Save("RadioButtonValue", "refill");
                                }
                                await this.DefaultAction.Execute();
                            });
                        })
                    }
                );
            }
            DropShadow.TranslateTo(3, 2, 0, null);
            //Button.Children.Add(DropShadow, 0, 0);
            Button.Children.Add(ButtonShape, 0, 0);

            this.Content.Children.Add(Button, 0, 0);
            this.Content.Children.Add(Label, 0, 0);
        }

        public void AddBorder(Color color, int thickness)
        {
            ButtonShape.BorderColor = color;
            ButtonShape.BorderWidth = thickness;
        }

        
        public void SetColour(Color colour)
        {
            this.ButtonShape.Color = colour;
        }

        public void SetText(string text)
        {
            this.Label.Text = text;
        }

        public void SetLayoutOptions(LayoutOptions horizontal, LayoutOptions vertical)
        {
            Button.HorizontalOptions = horizontal;
            Button.VerticalOptions = vertical;
            ButtonShape.HorizontalOptions = horizontal;
            ButtonShape.VerticalOptions = vertical;
        }

        public void SetSize(int width, int height)
        {
            Button.WidthRequest = width;
            Button.HeightRequest = height;
            ButtonShape.WidthRequest = width;
            ButtonShape.HeightRequest = height;

        }

        public void SetFontSize(int fontSize)
        {
            Label.FontSize = fontSize;
        }

        public void SetFont(string font, int fontSize)
        {
            Label.FontFamily = font;
            Label.FontSize = fontSize;
        }
    }
}
