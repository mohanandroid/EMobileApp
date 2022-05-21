using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eCups.Helpers
{
    public enum FontName
    {
        LatoBlack,
        LatoBlackItalic,
        LatoBold,
        LatoBoldItalic,
        LatoItalic,
        LatoLight,
        LatoLightItalic,
        LatoRegular,
        LatoThin,
        LatoThinItalic

    }

    public static class Fonts
    {
        static Dictionary<FontName, string> fontDictionary;

        public static void Init()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                fontDictionary = new Dictionary<FontName, string>
                {
                    [FontName.LatoBlack] = "Lato-Black.ttf#Lato-Black",
                    [FontName.LatoBlackItalic]="Lato-BlackItalic.ttf#Lato-BlackItalic",
                    [FontName.LatoBold] = "Lato-Bold.ttf#Lato-Bold",
                    [FontName.LatoBoldItalic] = "Lato-BoldItalic.ttf#Lato-BoldItalic",
                    [FontName.LatoItalic] = "Lato-Italic.ttf#Lato-Italic",
                    [FontName.LatoLight] = "Lato-Light.ttf#Lato-Light",
                    [FontName.LatoLightItalic] = "Lato-LightItalic.ttf#Lato-LightItalic",
                    [FontName.LatoRegular] = "Lato-Regular.ttf#Lato-Regular",
                    [FontName.LatoThin] = "Lato-Thin.ttf#Lato-Thin"

                };
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                fontDictionary = new Dictionary<FontName, string>
                {
                    [FontName.LatoBlack] = "Lato-Black",
                    [FontName.LatoBlackItalic] = "Lato-BlackItalic",
                    [FontName.LatoBold] = "Lato-Bold",
                    [FontName.LatoBoldItalic] = "Lato-BoldItalic",
                    [FontName.LatoItalic] = "Lato-Italic",
                    [FontName.LatoLight] = "Lato-Light",
                    [FontName.LatoLightItalic] = "Lato-LightItalic",
                    [FontName.LatoRegular] = "Lato-Regular",
                    [FontName.LatoThin] = "Lato-Thin"
                };
            }
        }

        public static string GetFont(FontName font)
        {
            return fontDictionary[font];
        }

        public static string GetRegularFont()
        {
            return fontDictionary[FontName.LatoRegular];
        }

        public static string GetBoldFont()
        {
            return fontDictionary[FontName.LatoBold];
        }
    }
}
