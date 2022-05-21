using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using eCups.Services;
using ZXing.Mobile;
using Xamarin.Forms;

[assembly: Dependency(typeof(eCups.Droid.Services.QrScanningService))]

namespace eCups.Droid.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            MobileBarcodeScanningOptions optionsDefault = new MobileBarcodeScanningOptions();
            MobileBarcodeScanningOptions customOptions = new MobileBarcodeScanningOptions();

            MobileBarcodeScanner scanner = new MobileBarcodeScanner();

            var scanResult = await scanner.Scan(customOptions);
            return scanResult.Text;
        }
    }
}
