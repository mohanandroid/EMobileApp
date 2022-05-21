using System;
using eCups.e.Images;
using eCups.Helpers;
using Xamarin.Forms;
using XFShapeView;

namespace eCups.e.Buttons
{
    public class IconButton : e.ActiveComponent
    {
        public Grid Button;
        public Label Label;
        public StaticImage Icon;
        public ShapeView ButtonShape;
        public StackLayout ContentContainer;

        public IconButton(int width, int height, Color backgroundColor, Color textColor, string buttonText, string iconSource, Models.Action action)
        {
            this.DefaultAction = action;

            this.Content = new Grid
            {
                WidthRequest = width,
                HeightRequest = height,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            ButtonShape = new ShapeView
            {
                ShapeType = ShapeType.Box,
                HeightRequest = height,
                WidthRequest = width,
                Color = backgroundColor,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 5,
                Opacity = 1
            };


            Button = new Grid
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = width,
                HeightRequest = height,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };


            //Button.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Units.TapSizeL) });
            //Button.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Icon = new StaticImage(
                    iconSource,
                    width,
                    height, null);

            Icon.Content.HorizontalOptions = LayoutOptions.Center;
            Icon.Content.Aspect = Aspect.AspectFit;
            

            Label = new Label
            {
                TextColor = textColor,
                Text = buttonText,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };

            Button.Children.Add(ButtonShape, 0, 0);

            ContentContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                //WidthRequest = Units.SmallButtonWidth,
                Padding = new Thickness(Units.ScreenUnitXS, 0),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent
            };

            ContentContainer.Children.Add(Icon.Content);
            //ContentContainer.Children.Add(Label);

            //Button.Children.Add(Icon.Content, 0, 0);
            //Button.Children.Add(Label, 1, 0);
            Button.Children.Add(ContentContainer, 0, 0);
            //Grid.SetColumnSpan(ButtonShape, 2);
            //Grid.SetColumnSpan(ContentContainer, 2);


            if (this.DefaultAction != null)
            {
                this.Content.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await Update();
                                await DefaultAction.Execute();
                            });
                        })
                    }
                );
            }

            this.Content.Children.Add(Button, 0, 0);
        }

        public void SetPositionLeft()
        {
            Content.HorizontalOptions = LayoutOptions.StartAndExpand;
        }

        public void SetPositionCenter()
        {
            Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
        }

        public void SetPositionRight()
        {
            Content.HorizontalOptions = LayoutOptions.EndAndExpand;
        }

        public void SetContentLeft()
        {
            //Content.HorizontalOptions = LayoutOptions.StartAndExpand;
            Button.HorizontalOptions = LayoutOptions.StartAndExpand;
            ContentContainer.HorizontalOptions = LayoutOptions.StartAndExpand;
        }

        public void SetContentCenter()
        {
            //Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            Button.HorizontalOptions = LayoutOptions.CenterAndExpand;
            ContentContainer.HorizontalOptions = LayoutOptions.CenterAndExpand;
        }

        public void SetContentRight()
        {
            //Content.HorizontalOptions = LayoutOptions.EndAndExpand;
            Button.HorizontalOptions = LayoutOptions.EndAndExpand;
            ContentContainer.HorizontalOptions = LayoutOptions.EndAndExpand;
        }
    }
}
