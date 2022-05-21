using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Layouts.Custom
{
    public class SubHeader : StandardLayout
    {
        ActiveImage Logo;
        IconButton MenuButton;

        public SubHeader()
        {
            Height = Units.TapSizeXS;
            Width = Units.ScreenWidth;
            TransitionTime = 150;
            TransitionType = (int)AppSettings.TransitionTypes.SlideOutRight;

            Content = new Grid();

            Container = new Grid
            {
                WidthRequest = Width,
                HeightRequest = Height,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(Units.ScreenHeight5Percent, 0, Units.ScreenWidth10Percent, 1),
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.FromHex(Colors.BH_GOLD)
            };            

            Content.Children.Add(Container, 0, 0);
        }
    }
}