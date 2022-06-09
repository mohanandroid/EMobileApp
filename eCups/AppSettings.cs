using System;
using eCups.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace eCups
{
    public class AppSettings
    {
        // App specific settings
        public static bool Debug = false;

        public static string TwitterUrl = "https://twitter.com/";
        public static string LinkedInUrl = "https://www.linkedin.com/";
        public static string FacebookUrl = "https://www.facebook.com/";
        public static bool openLinksInApp = false; //if false redirects to browser
        public static Location userLocation;
        public static int activeStore = 0;

        public static string QRPreCode = "!!eCups!!::";

        public static int FirstPage;

        // Page identifiers
        public enum PageNames
        {
            FirstLoad,
            CafePage,
            ChangeLocation,
            DealsLanding,  
            Home,
            Landing,
            Map,
            Menu,
            QRScanner,
            SignUp,
            StoresLanding,
            Template,
            Welcome,
            WelcomeLogin,
            YourAccount,
            YourRewards,
            AboutUs,
            AccountCreated,
            NewLevel,
            CodeScanned,
            EmailVerficiation
        };

        public static int TransitionSlow = 1500;
        public static int TransitionMedium = 1000;
        public static int TransitionFast = 500;
        public static int TransitionVeryFast = 250;

        // General app settings
        public bool HasStatusBar { get; set; }
        public bool HasHeader { get; set; }
        public bool HasSubHeader { get; set; }
        public bool HasNavHeader { get; set; }
        public bool HasFooter { get; set; }

        public int FullScreenHeight { get; set; }
        public int StatusBarHeight { get; set; }
        public int HeaderHeight { get; set; }
        public int FooterHeight { get; set; }

        // modal
        public float ModalOpacity { get; set; }

        // menu options
        public int MenuWidth { get; set; }
        public int MenuHeight { get; set; }
        public int MenuCoverageHorizontal { get; set;}
        public int MenuCoverageVertical { get; set; }
        public bool MenuShownOverContent { get; set; }


        public enum MenuPositions
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public enum TransitionDirections
        {
            SlideOutTop,
            SlideOutBottom,
            SlideOutLeft,
            SlideOutRight,
            SlideInFromTop,
            SlideInFromBottom,
            SlideInFromLeft,
            SlideInFromRight
        }

        public enum TransitionTypes
        {
            SlideOutLeft,
            SlideOutRight,
            SlideOutTop,
            SlideOutBottom,
            FadeOut,
            ScaleOut
        }

        public enum RequestIds
        {
            RegisterUser,
            LoginUser,
            LogoutUser,
            GetSomething, // etc
        }

        public bool OverlayMenu { get; set; }
        public int MenuPosition { get; set; }

        public static string URL_FACEBOOK = "https://www.facebook.com/";
        public static string URL_TWITTER = "https://twitter.com/";

        public AppSettings()
        {
            HeaderHeight = Units.HeaderHeight;
            FooterHeight = Units.FooterHeight;
            ModalOpacity = 0.75f;
            HasStatusBar = true;
            HasHeader = true;
            HasSubHeader = true;
            HasNavHeader = true;
            HasFooter = false;

            OverlayMenu = true;
            MenuWidth = (int)(Units.ScreenWidth);
            MenuHeight = Units.ScreenHeight;
            MenuPosition = (int)MenuPositions.Left;
            MenuCoverageHorizontal = MenuWidth;// (int)(Units.ScreenWidth*0.6);
            MenuCoverageVertical = MenuHeight;// (int)(Units.ScreenHeight * 0.6);
            MenuShownOverContent = true;

            // Android
            StatusBarHeight = 0;

            // iOS
            if (Device.RuntimePlatform == Device.iOS)
            {
                StatusBarHeight = 0;
            }

            FirstPage = (int)AppSettings.PageNames.FirstLoad;
        }
    }
}
