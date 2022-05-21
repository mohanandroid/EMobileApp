using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using UIKit;
using UserNotifications;
using XFShapeView.iOS;
//using ZXing.Mobile;
using ZXing.Net.Mobile;

namespace eCups.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        //public App _app;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            global::Xamarin.Forms.Forms.Init();

            //KeyboardOverlapRenderer.Init();

            //ButtonCircleRenderer.Init();
            ShapeRenderer.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();



            //LoadApplication(new App((int)UIScreen.MainScreen.Bounds.Width, (int)UIScreen.MainScreen.Bounds.Height));
            //_app = new App((int)UIScreen.MainScreen.Bounds.Width, (int)UIScreen.MainScreen.Bounds.Height);

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            LoadApplication(new App((int)UIScreen.MainScreen.Bounds.Width, (int)UIScreen.MainScreen.Bounds.Height));

            App.SetPixelSize((int)UIScreen.MainScreen.NativeBounds.Width, (int)UIScreen.MainScreen.NativeBounds.Height);
            App.SetScalables((int)UIScreen.MainScreen.Scale);


            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request Permissions
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
                {
                    // Do something if needed
                });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                 UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                    );

                app.RegisterUserNotificationSettings(notificationSettings);
            }
            app.StatusBarStyle = UIStatusBarStyle.LightContent;
            return base.FinishedLaunching(app, options);
        }
    }
}
