using System;
using eCups.Branding;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Layouts.Custom.Tiles
{
    public class ScanTile : StandardLayout
    {
        public ScanTile()
        {
            Content = BuildLayout();
        }

        private Grid BuildLayout()
        {
            StaticLabel ScanQrCodeLabel = new StaticLabel("Scan the QR Code");
            ScanQrCodeLabel.Content.FontSize = Units.FontSizeXL;

            StaticLabel ScanQrCodeDetail = new StaticLabel("Scan the QR code on the bottom of the Ecup you have purchased to register the cup to your account");
            ScanQrCodeDetail.LeftAlign();
            ScanQrCodeDetail.Content.TextColor = Color.FromHex(Colors.EC_GREEN_2);
            ScanQrCodeDetail.Content.FontSize = Units.FontSizeL;

            Grid ScanContent = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(Units.ScreenWidth10Percent)
            };

            StackLayout SectionContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Spacing = 16
            };

            StackLayout IconsContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 192,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 0
            };

            Grid ScanButtonLayout = new ScanButton().Content;

            /*
            Grid ScanButtonLayout = new Grid
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
                               //await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner);
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
            */
            StaticImage CupImage = new StaticImage("cup.png", 92, null);

            IconsContainer.Children.Add(CupImage.Content);
            IconsContainer.Children.Add(ScanButtonLayout);


            SectionContainer.Children.Add(ScanQrCodeLabel.Content);
            SectionContainer.Children.Add(ScanQrCodeDetail.Content);
            SectionContainer.Children.Add(IconsContainer);

            ScanContent.Children.Add(SectionContainer);

            return ScanContent;
        }
    }
}
