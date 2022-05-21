using System;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Layouts.Custom
{
    public class Paragraph
    {
        public StackLayout Content { get; set; }
        public StaticLabel Header { get; set; }
        public StaticLabel MainContent { get; set; }
        public StaticImage Image { get; set; }

        public Paragraph(string header, string mainContent, string imageUrl)
        {
            Content = new StackLayout { Orientation = StackOrientation.Vertical, Spacing = 0, Margin = new Thickness(0, Units.ScreenUnitS) };

            Header = null;
            MainContent = null;
            Image = null;

            if (header != null)
            {
                Header = new StaticLabel(header);
                Header.Content.TextColor = Color.FromHex(Colors.BH_LIGHT_BLUE);
                Header.Content.FontSize = Units.FontSizeXL;
                Header.Content.FontFamily = Fonts.GetBoldFont();

                Content.Children.Add(Header.Content);
            }

            if (mainContent != null)
            {
                MainContent = new StaticLabel(mainContent);
                MainContent.Content.TextColor = Color.FromHex(Colors.BH_DARK_GREY);
                MainContent.Content.FontSize = Units.FontSizeL;

                Content.Children.Add(MainContent.Content);
            }

            if (imageUrl != null)
            {
                Image = new StaticImage(imageUrl, Units.ScreenWidth, (int)(Units.ScreenWidth*0.6), null);
                Image.Content.VerticalOptions = LayoutOptions.CenterAndExpand;

                Content.Children.Add(Image.Content);
            }
        }
    }
}
