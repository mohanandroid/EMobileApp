using System;
using eCups.Branding;
using eCups.e;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Helpers.Custom;
using MagicGradients;
using Xamarin.Forms;

namespace eCups.Components.Buttons
{
    public class MenuHudButton : ActiveComponent
    {
        public StackLayout buttonContents;
        StaticImage Icon;
        StaticLabel Name;
        CFrame gradientButton;

        public MenuHudButton(string buttonText, string iconSource, Models.Action action)
        {

            Content = new Grid
            {
                HeightRequest = 70,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Content.GestureRecognizers.Add(
               new TapGestureRecognizer()
               {
                   Command = new Command(() =>
                   {
                       Device.BeginInvokeOnMainThread(async () =>
                       {
                           Console.WriteLine("Pressed");
                           await App.PerformActionAsync(action);
                       });
                   })
               }
            );

            buttonContents = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };


            gradientButton = new CFrame
            {
                HeightRequest = 62,
                WidthRequest = 128,
                CornerRadius = 34,
                BorderColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                BorderWidth = 4,
                GradientStartColor = Color.FromHex(Colors.EC_GRADIENT_START),
                GradientEndColor = Color.FromHex(Colors.EC_GRADIENT_END),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsClippedToBounds = false,
                HasShadow = false
            };

            Icon = new StaticImage(iconSource, 25, null);

            Icon.Content.HorizontalOptions = LayoutOptions.Center;

            Name = new StaticLabel(buttonText);
            Name.CenterAlign();
            Name.Content.TextColor = Color.White;
            Name.Content.FontSize = 12;

            buttonContents.Children.Add(Icon.Content);
            buttonContents.Children.Add(Name.Content);

            Content.Children.Add(gradientButton, 0, 0);
            Content.Children.Add(buttonContents, 0, 0);

        }

        public void SetGradient(float startX, float startY, float endX, float endY)
        {
            gradientButton.GradientStartX = startX;
            gradientButton.GradientStartY = startY;
            gradientButton.GradientEndX = endX;
            gradientButton.GradientEndY = endY;
        }
    }
}

