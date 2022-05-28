using System;
using eCups.Branding;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Services.Storage;
using Xamarin.Forms;

namespace eCups.Layouts.Custom.Tiles
{
    public class ScanButton : StandardLayout
    {
        public Grid ScanButtonLayout { get; set; }


        public ScanButton()
        {
            Content = BuildLayout();
        }

        private Grid BuildLayout()
        {
            ScanButtonLayout = new Grid
            {
                HeightRequest = 140,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(24, 16, 0, 0)
            };

            ScanButtonLayout.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               //AppSession.SignUpStage = 1;
                               LocalDataStore.Clear("Page");
                               await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner);
                               //NextSection();
                           });
                       })
                   }
               );

            ScanButtonLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            ScanButtonLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });


            Frame ScanBackground = new Frame
            {
                BackgroundColor = Color.FromHex(Colors.EC_GREEN_2),
                WidthRequest = 240,
                HeightRequest = 140,
                CornerRadius = 24,
                HasShadow = false,
                VerticalOptions = LayoutOptions.CenterAndExpand

            };

            StaticImage CameraImage = new StaticImage("camera.png", 64, null);
            CameraImage.Content.Margin = new Thickness(0, 16, 0, 4);

            StaticLabel ScanQrCodeButtonText = new StaticLabel("Scan QR Code");
            ScanQrCodeButtonText.Content.TextColor = Color.White;
            ScanQrCodeButtonText.Content.Margin = new Thickness(0, 4, 0, 16);
            ScanQrCodeButtonText.CenterAlign();

            ScanButtonLayout.Children.Add(ScanBackground, 0, 0);
            ScanButtonLayout.Children.Add(CameraImage.Content, 0, 0);
            ScanButtonLayout.Children.Add(ScanQrCodeButtonText.Content, 0, 1);

            Grid.SetRowSpan(ScanBackground, 2);

            
            return ScanButtonLayout;
        }

        
    }
}
