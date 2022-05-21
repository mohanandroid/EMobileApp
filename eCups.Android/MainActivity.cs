using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Plugin.Toasts;
using Refractored.XamForms.PullToRefresh.Droid;
using ZXing.Mobile;
using Android;
using Acr.UserDialogs;

namespace eCups.Droid
{
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private App _app;

        //Location Permissions
        const int RequestLocationId = 0;
        readonly string[] Permissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.Camera
        };

        protected override void OnStart()
        {
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted || CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted)
                {
                    RequestPermissions(Permissions, RequestLocationId);
                }
                else
                {
                    // Permissions already granted - display a message.
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    // Permissions granted - display a message.
                }
                else
                {
                    // Permissions denied - display a message.
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            PullToRefreshLayoutRenderer.Init();

            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.Window.ClearFlags(WindowManagerFlags.Fullscreen);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.Forms.Forms.SetTitleBarVisibility(global::Xamarin.Forms.AndroidTitleBarVisibility.Never);

            DependencyService.Register<ToastNotification>();
            ToastNotification.Init(this, new PlatformOptions() { SmallIconDrawable = Android.Resource.Drawable.IcDialogInfo });

            UserDialogs.Init(this);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            //CrossCurrentActivity.Current.Activity = this;

            #region For screen Height & Width  
            var pixels = Resources.DisplayMetrics.WidthPixels;
            var scale = Resources.DisplayMetrics.Density;
            var dps = (double)((pixels - 0.5f) / scale);
            var ScreenWidth = (int)dps;

            pixels = Resources.DisplayMetrics.HeightPixels;
            dps = (double)((pixels - 0.5f) / scale);
            var ScreenHeight = (int)dps;

            //_app = new App((int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density), (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density));
            _app = new App(ScreenWidth, ScreenHeight);

            //_app = new App((int)Resources.DisplayMetrics.WidthPixels, (int)Resources.DisplayMetrics.HeightPixels);

            //App.ScreenHeight = ScreenHeight;
            //App.screenWidth = ScreenWidth;
            #endregion

            Xamarin.Essentials.Platform.Init(this.Application);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            MobileBarcodeScanner.Initialize(this.Application);
            LoadApplication(_app);

            Console.WriteLine("Screen Size: " + ScreenWidth + " x " + ScreenHeight);
            Console.WriteLine("Pixel Size: " + (int)(Resources.DisplayMetrics.WidthPixels) + " x " + (int)(Resources.DisplayMetrics.HeightPixels));
            Console.WriteLine("Scaled Size: " + (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density) + " x " + (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density));

            Console.WriteLine("Density: " + (int)(Resources.DisplayMetrics.Density));
            Console.WriteLine("Scaled Density: " + (int)(Resources.DisplayMetrics.ScaledDensity));
            Console.WriteLine("Xdpi: " + (int)(Resources.DisplayMetrics.Xdpi));
            Console.WriteLine("Ydpi: " + (int)(Resources.DisplayMetrics.Ydpi));

            Console.WriteLine("Device Type: " + Device.Idiom);
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                App.SetScalables(2);
            }

            App.SetPixelSize((int)(Resources.DisplayMetrics.WidthPixels), (int)(Resources.DisplayMetrics.HeightPixels));
            //App.SetScalables((int)(Resources.DisplayMetrics.Density));
        }


        public override void OnBackPressed()
        {
            if (_app.HandleHardwareBack().Result == false)
            {
                base.OnBackPressed();
            }
            else
            {
                return;
            }
        }
    }
}