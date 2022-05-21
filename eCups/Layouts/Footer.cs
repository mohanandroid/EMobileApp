using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.Components.Buttons;
using eCups.e.Buttons;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Layouts
{
    public class Footer
    {
        public Grid Content { get; set; }
        public int Height { get; set; }
        public uint TransitionTime { get; set; }

        public Footer()
        {
            TransitionTime = 100;

            Content = new Grid
            {
                BackgroundColor = Color.FromHex(Colors.EC_GREEN_2)
            };
            MenuHudButton MenuButton = new MenuHudButton("Menu", "close.png", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Menu));
            ScanHudButton ScannerButton = new ScanHudButton(/*"QR Scanner", "", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner)*/);

            Content.Children.Add(MenuButton.Content);
            Content.Children.Add(ScannerButton.Content);

        }

        public void SetHeight(int height)
        {
            Height = height;
            Content.HeightRequest = height;
        }

        public async Task<bool> Show()
        {
            Content.IsVisible = true;
            await Task.WhenAll(
                Content.TranslateTo(0, 0, TransitionTime, Easing.Linear),
                Content.FadeTo(1, TransitionTime, Easing.Linear)
                );
            return true;
        }

        public async Task<bool> Hide()
        {
            await Task.WhenAll(
                Content.TranslateTo(0, Height, TransitionTime, Easing.Linear),
                Content.FadeTo(1, TransitionTime, Easing.Linear)
                );
            Content.IsVisible = false;
            return true;
        }
    }
}
