using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;
using eCups.Services;
using eCups.Renderers;
using ZXing.Net.Mobile.Forms;
using System.Diagnostics;

namespace eCups.Pages.Custom
{
    public class QRScanner : Page
    {
        StackLayout ContentContainer;
        Grid Header;
        StaticImage MainLogo;
        IconButton ExitButton;
        Label debugText;
        ZXingScannerView cam;
        StaticImage camFocus;

        public QRScanner()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.QRScanner;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            //AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);

            cam = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsEnabled = true,
                IsScanning = true,
            };

            cam.OnScanResult += Cam_OnScanResult;

            AddBackgroundView(cam);

            Thickness decorThickness = new Thickness(0, -150, 0, 0); //Margin Adjustment for Decor
            AddDecor("top_decor.png", Units.ScreenHeight40Percent, LayoutOptions.Start, decorThickness);
            decorThickness = new Thickness(0, 0, 0, -160); //Margin Adjustment for Decor
            AddDecor("bottom_decor3.png", Units.ScreenHeight40Percent, LayoutOptions.End, decorThickness);

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
            PageContent.Children.Add(camFocus.Content);
        }

        public StackLayout BuildContent()
        {
            // build labels
            StackLayout mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight
            };

            Header = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };

            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenWidth5Percent, Units.ScreenHeight5Percent, 0, 0);

            ExitButton = new IconButton(28, 28, Color.Transparent, Color.White, "Scan QR Code", "close.png", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Home));
            ExitButton.Content.VerticalOptions = LayoutOptions.Center;
            ExitButton.Content.HorizontalOptions = LayoutOptions.End;
            ExitButton.Content.Margin = new Thickness(0, 0, Units.ScreenWidth10Percent, 0);

            Header.Children.Add(MainLogo.Content, 0, 0);
            Header.Children.Add(ExitButton.Content, 1, 0);

            mainLayout.Children.Add(Header);

            camFocus = new StaticImage("qrScanFrame.png", (int)(Units.ScreenWidth * 0.8), null);
            camFocus.Content.HorizontalOptions = LayoutOptions.Center;
            camFocus.Content.VerticalOptions = LayoutOptions.Center;
            camFocus.Content.Opacity = 0.4;

            
            //camFocus.Content.TranslateTo(Units.HalfScreenWidth, Units.HalfScreenHeight, 0, null);

            return mainLayout;
        }

        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);

                this.HasHeader = false;
                this.HasFooter = true;
                await base.Update();

                PageContent.Children.Remove(ContentContainer);
                ContentContainer = BuildContent();
                PageContent.Children.Add(ContentContainer);
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }

        private async void Cam_OnScanResult(ZXing.Result result)
        {
            //debug actions
            Debug.WriteLine("QR Data: " + result.Text);
            if(result.Text.Contains(AppSettings.QRPreCode))
            {
                //TODO check validity of QR Code here
                if (true)
                {
                    cam.IsScanning = false;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.CodeScanned);
                    });
                }
            }
        }
    }
}
