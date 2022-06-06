using System;
using System.Collections.Generic;
using eCups.Branding;
using eCups.e.Composites;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Services.Storage;
using FFImageLoading.Transformations;
using Xamarin.Forms;

namespace eCups.e.Buttons
{
    public class MenuButton : e.ActiveComponent
    {
        public Grid Button;
        public Label Label;
        public StaticImage Icon;
        public Grid ContentContainer;
        public Devider devider;

        public MenuButton(Color iconColourOverlay, Color textColor, string buttonText, string iconSource, int iconWidth, Models.Action action)
        {
            this.DefaultAction = action;

            this.Content = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0,0,0,7.5f)
            };


            Button = new Grid
            {
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Button.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            Button.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            List<FFImageLoading.Work.ITransformation> transformations = new List<FFImageLoading.Work.ITransformation>();
            TintTransformation tint = new TintTransformation
            {
                HexColor = iconColourOverlay.ToHex(),
                EnableSolidColor = true,
            };
            transformations.Add(tint);

            Icon = new StaticImage(
                    iconSource,
                    iconWidth,
                    iconWidth, transformations);

            Icon.Content.VerticalOptions = LayoutOptions.Start;
            Icon.Content.HorizontalOptions = LayoutOptions.Center;
            Icon.Content.Aspect = Aspect.AspectFit;
            Icon.Content.Margin = new Thickness(0, 0, 0, 5);

            Label = new Label
            {
                TextColor = textColor,
                Text = buttonText,
                FontSize = 16,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(iconWidth,0,0,5)
            };


            ContentContainer = new Grid
            {
                //WidthRequest = Units.SmallButtonWidth,
                Padding = new Thickness(Units.ScreenUnitS, 0),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.Transparent
            };

            ContentContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(iconWidth, GridUnitType.Absolute)  });
            ContentContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            ContentContainer.Children.Add(Icon.Content, 0, 0);
            ContentContainer.Children.Add(Label, 1, 0);

            devider = new Devider(Color.White, LayoutOptions.EndAndExpand, 2, 0.5f);
            devider.Content.Margin = new Thickness(iconWidth*2+Units.ScreenUnitS, 0, 0, 0);

            Button.Children.Add(ContentContainer, 0, 0);
            Button.Children.Add(devider.Content, 0, 1);

            if (this.DefaultAction != null)
            {
                Button.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await Update();
                                if (buttonText == "QR Scanner")
                                {
                                    LocalDataStore.Clear("Qrcode");
                                    LocalDataStore.Save("RadioButtonValue", "buy");
                                }
                                await DefaultAction.Execute();
                            });
                        })
                    }
                );
            }

            this.Content.Children.Add(Button, 0, 0);
        }

        public void HideDevider()
        {
            devider.Content.Opacity = 0;
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
