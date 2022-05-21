using System;
using eCups.Branding;
using eCups.e.Images;
using eCups.e.Labels;
using MagicGradients;
using Xamarin.Forms;

namespace eCups.e.Buttons
{
    public class ScanHudButton : ActiveComponent
    {
        StaticImage Icon;
        StaticLabel Name;

        public ScanHudButton()
        {
            Container = new Grid { };
            Content = new Grid { };

           
            GradientElements<GradientStop> gradientStops = new GradientElements<GradientStop>();
            gradientStops.Add(new GradientStop { Color = Color.FromHex(Colors.EC_GRADIENT_START) });
            gradientStops.Add(new GradientStop { Color = Color.FromHex(Colors.EC_GRADIENT_END) });


            GradientView gradient = new GradientView
            {
                GradientSource = new LinearGradient
                {
                    Angle = 90,
                    Stops = gradientStops
                },
                HeightRequest = 64,
                WidthRequest = 128,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand

            };
            Container.Children.Add(gradient, 0, 0);
            Content.Children.Add(Container);
        }
    }
}
