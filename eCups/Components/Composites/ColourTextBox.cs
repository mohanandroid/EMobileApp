using System;
using eCups.Helpers;
using eCups.Tools;
using Xamarin.Forms;
using XFShapeView;

namespace eCups.e.Buttons
{
    public class ColourTextBox : e.ActiveComponent
    {
        public Grid Box;
        public Label Label;
        public ShapeView BoxShape;

        public ColourTextBox(Color backgroundColor, Color textColor, int width, int height, string buttonText, Models.Action action)
        {
            this.DefaultAction = action;

            this.Content = new Grid
            {
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.Center
            };

            BoxShape = new ShapeView
            {
                ShapeType = ShapeType.Box,
                HeightRequest = height,
                WidthRequest = width,
                Color = backgroundColor,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 16,//Units.TapSizeXS/2,
                Opacity = 1
            };


            Box = new Grid
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = BoxShape.Width,
                HeightRequest = BoxShape.Height,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Label = new Label
            {
                TextColor = textColor,
                Text = buttonText,
                HeightRequest = BoxShape.Height,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = Fonts.GetRegularFont(),
                Margin = Units.ScreenUnitS
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
                                await this.DefaultAction.Execute();
                            });
                        })
                    }
                );
            }

            Box.Children.Add(BoxShape, 0, 0);
            this.Content.Children.Add(Box, 0, 0);
            this.Content.Children.Add(Label, 0, 0);
        }

        public void SetColour(Color colour)
        {
            this.BoxShape.BackgroundColor = colour;
        }

        public void SetText(string text)
        {
            this.Label.Text = text;
        }
    }
}
