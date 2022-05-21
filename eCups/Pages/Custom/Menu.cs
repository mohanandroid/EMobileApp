using System;
using System.Threading.Tasks;
using eCups.AppData;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class Menu : Page
    {
        StackLayout ContentContainer;
        private MenuHeader MainHeader;

        MenuButton CloseButton;
        StackLayout MenuItems;
        MenuButton Home;
        MenuButton Rewards;
        MenuButton Offers;
        MenuButton Stores;
        MenuButton Map;
        MenuButton QRScanner;
        MenuButton Account;
        MenuButton Settings;

        
        public Menu()
        {
            this.IsScrollable = false;
            this.Id = (int)AppSettings.PageNames.Menu;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;
            

            PageContent = new Grid
            {
                BackgroundColor = Color.FromHex(Colors.BH_DARK_GREY),
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);

            // build labels
            ContentContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Transparent,//FromHex(Colors.No),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight,
            };

            MainHeader = new MenuHeader();

            ContentContainer.Children.Add(MainHeader.Content);

            AddDecor("bottom_decor3.png", Units.ScreenHeight40Percent, LayoutOptions.End, new Thickness(0,0,0,-Units.ScreenHeight15Percent));

            MenuItems = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(Units.ScreenWidth10Percent, Units.ScreenHeight5Percent, Units.ScreenWidth30Percent,Units.ScreenHeight5Percent)
            };

            Home = new MenuButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "Home", "home.png", 25, new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Home));
            Rewards = new MenuButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "Your Rewards", "loyalty.png", 25, new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.YourRewards));
            Offers = new MenuButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "Ecup Offers", "offer.png", 25, new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.DealsLanding));
            Stores = new MenuButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "Ecup Stores", "cup_icon.png", 25 ,new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.StoresLanding));
            QRScanner = new MenuButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "QR Scanner", "camera.png", 25, new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner));
            Account = new MenuButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "Your Account", "account.png", 25, new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.YourAccount));
            //Settings = new MenuButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "App Settings", "settings.png", 25, new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.AboutUs));
            Account.HideDevider();

            MenuItems.Children.Add(Home.Content);
            MenuItems.Children.Add(Rewards.Content);
            MenuItems.Children.Add(Offers.Content);
            MenuItems.Children.Add(Stores.Content);
            MenuItems.Children.Add(QRScanner.Content);
            MenuItems.Children.Add(Account.Content);
            //MenuItems.Children.Add(Settings.Content);
            ContentContainer.Children.Add(MenuItems);


            PageContent.Children.Add(ContentContainer);
            
        }

        public override async Task Update()
        {
            await DebugUpdate(AppSettings.TransitionVeryFast);
        }
    }
}
