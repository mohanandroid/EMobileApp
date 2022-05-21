using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCups.AppData;
using eCups.DebugData.Custom;
using eCups.Helpers;
using eCups.Services;
using eCups.Services.Api;
using eCups.Services.Storage;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace eCups
{
    public class App : Application
    {
        static AppContainter AppContainer;
        public static ApiBridge ApiBridge;

        public static bool IsBusy { get; set; }

        public App(int width, int height)
        {
            // initialise api bridge
            ApiBridge = new ApiBridge(); // use this for live api

            // set up UI units
            Units.Init(width, height);

            // initialise the app session
            AppSession.Init();

            // initialise static data (data loaded from assets contained within the app - data, text, etc)
            StaticData.Init();

            // initialise debug data
            FakeData.Init(); // unused for now

            // initialise local storage
            LocalDataStore.Init();

            // initialise actions
            Actions.Init();

            // initialise fonts
            Fonts.Init();

            // initialise pages
            Helpers.Pages.Init();

            if (LocalDataStore.LoadUser() != null)
            {
                AppSession.CurrentUser = LocalDataStore.LoadUser();
            }
            else
            {
#if DEBUG
                //AppSession.CurrentUser = new Models.User
                //{
                //    FirstName = "Mat",
                //    LastName = "Howler",
                //    EmailAddress = "MatHowler@rockingaway.co.uk",
                //    Username = "MetalMental1976",
                //    Password = "1amTh3K1ng0fR0ck",
                //    DateOfBirth = DateTime.UtcNow.AddYears(-20),
                //    MobileNumber = "07542 807060",
                //    Address = new Models.Address
                //    {
                //        AddressLine1 = "123 Imagine Way",
                //        AddressLine2 = "",
                //        AreaCode = "N0T R34L",
                //        City = "Not A Place",
                //        Country = "A Country",
                //        County = "Imagine County"
                //    }
                //};
#else
       AppSession.CurrentUser = new Models.User();
#endif

            }

            // create app container
            AppContainer = new AppContainter(width, height);

            //Device.StartTimer(TimeSpan.FromMilliseconds(25), Update);

            //DependencyService.Get<IStatusBarStyleManager>().SetLightTheme();

            //AppContainer.BackgroundColor = Color.Red;

            MainPage = AppContainer;
        }

        public static void SetScalables(int scale)
        {
            Units.SetScalableUnits(scale);
        }

        public static void SetPixelSize(int pixelWidth, int pixelHeight)
        {
            Units.SetPixelSize(pixelWidth, pixelHeight);
        }

        public static async Task PerformActionAsync(Models.Action action)
        {
            if (IsBusy)
            {
                return;
            }

            // this action takes us to another page
            if (action.Id == (int)Actions.ActionName.GoToPage && action.TargetPageId > -1)
            {
                AppContainer.SetNextPage(action.TargetPageId);
            }

            if (action.Id == (int)Actions.ActionName.OpenMap && action.Param != null)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    await Launcher.OpenAsync("http://maps.apple.com/?" + action.Param);
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    await Launcher.OpenAsync("http://maps.google.com/?" + action.Param);
                }
            }

            if (action.Id == (int)Actions.ActionName.GoToStore)
            {
                AppSettings.activeStore = action.TargetPageId; //set here to call right data on CafePage
                AppContainer.SetNextPage((int)AppSettings.PageNames.CafePage);
            }

            await Execute(action.Id).ConfigureAwait(false);
        }

        public static async Task PerformActionAsync(int actionId, int targetPageId)
        {
            if (IsBusy)
            {
                return;
            }

            // this action takes us to another page
            if (actionId == (int)Actions.ActionName.GoToPage && targetPageId > -1)
            {
                AppContainer.SetNextPage(targetPageId);
            }

            if (actionId == (int)Actions.ActionName.GoToNextPage && targetPageId > -1)
            {
                AppContainer.SetNextPage(targetPageId);
            }

            if (actionId == (int)Actions.ActionName.GoToStore && targetPageId > -1)
            {
                AppSettings.activeStore = targetPageId; //set here to call right data on CafePage
                AppContainer.SetNextPage((int)AppSettings.PageNames.CafePage);
            }

            await Execute(actionId).ConfigureAwait(false);
        }

        public static async Task PerformActionAsync(int actionId, string addressPoints)
        {
            if (actionId == (int)Actions.ActionName.OpenMap && addressPoints != null)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    await Launcher.OpenAsync("http://maps.apple.com/?" + addressPoints);
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    await Launcher.OpenAsync("http://maps.google.com/?" + addressPoints);
                }
            }
        }

        public static async Task ShowMenu()
        {
            await Task.Delay(10);
            await AppContainer.ShowMenu();
        }

        public static async Task CloseMenu()
        {
            await Task.Delay(10);
            await AppContainer.HideMenu();
        }

        public static async Task OpenMenu()
        {
            await Task.Delay(10);
            await AppContainer.ShowMenu();
            //AppContainer.SetLastPage(AppSession.PageBeforeMenu);
            //AppContainer.GoToLastPage();
            //AppContainer.GoToLastPageFromMenu()
        }

        public static void ShowMenuButton()
        {
            AppContainer.ShowMenuButton();
        }

        public static void HideMenuButton()
        {
            AppContainer.HideMenuButton();
        }

        public static void ShowHeader()
        {
            AppContainer.ShowHeader();
        }

        public static void HideHeader()
        {
            AppContainer.HideHeader();
        }

        public static void ShowSubHeader()
        {
            AppContainer.ShowSubHeader();
        }

        public static void HideSubHeader()
        {
            AppContainer.HideSubHeader();
        }

        public static void ShowNavHeader()
        {
            AppContainer.ShowNavHeader();
        }

        public static void HideNavHeader()
        {
            AppContainer.HideNavHeader();
        }

        public static void ShowFooter()
        {
            AppContainer.ShowFooter();
        }

        public static void HideFooter()
        {
            AppContainer.HideFooter();
        }

        public static async Task<bool> RefreshData(int pageId, int dataRequestId)
        {
            await Task.Delay(10);

            return true;
        }

        private static async Task<bool> Execute(int actionId)
        {
            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            await Task.Delay(10);

            Console.WriteLine("Performing action: " + actionId);

            switch (actionId)
            {
                case (int)Actions.ActionName.GoToPage:
                    Helpers.Pages.TransitionAction = (int)Helpers.Pages.TransitionActions.Direct;
                    await AppContainer.GoToNextPage();
                    break;
                case (int)Actions.ActionName.GoToNextPage:
                    await AppContainer.GoToNextPage();
                    break;
                case (int)Actions.ActionName.GoToLastPage:
                    await AppContainer.GoToLastPage();
                    break;
                case (int)Actions.ActionName.QuitApp:
                    break;
                case (int)Actions.ActionName.ShowLoading:
                    await ShowLoading();
                    break;
                case (int)Actions.ActionName.HideLoading:
                    await HideLoading();
                    break;
                case (int)Actions.ActionName.ShowMenu:
                    await AppContainer.ShowMenu();
                    break;
                case (int)Actions.ActionName.HideMenu:
                    await AppContainer.HideMenu();
                    break;
                case (int)Actions.ActionName.ToggleMenu:
                    await AppContainer.ToggleMenu();
                    break;
                case (int)Actions.ActionName.ShowHeader:
                    await AppContainer.ShowHeader();
                    break;
                case (int)Actions.ActionName.HideHeader:
                    await AppContainer.HideHeader();
                    break;
                case (int)Actions.ActionName.ToggleHeader:
                    await AppContainer.ToggleHeader();
                    break;
                case (int)Actions.ActionName.ShowFooter:
                    await AppContainer.ShowFooter();
                    break;
                case (int)Actions.ActionName.HideFooter:
                    await AppContainer.HideFooter();
                    break;
                case (int)Actions.ActionName.ToggleFooter:
                    await AppContainer.ToggleFooter();
                    break;
                case (int)Actions.ActionName.ShowSubHeader:
                    await AppContainer.ShowSubHeader();
                    break;
                case (int)Actions.ActionName.HideSubHeader:
                    await AppContainer.HideSubHeader();
                    break;
                case (int)Actions.ActionName.ToggleSubHeader:
                    await AppContainer.ToggleSubHeader();
                    break;
                case (int)Actions.ActionName.ShowNavHeader:
                    await AppContainer.ShowNavHeader();
                    break;
                case (int)Actions.ActionName.HideNavHeader:
                    await AppContainer.HideNavHeader();
                    break;
                case (int)Actions.ActionName.ToggleNavHeader:
                    await AppContainer.ToggleNavHeader();
                    break;
                case (int)Actions.ActionName.ShowModal:
                    await AppContainer.ShowModal();
                    break;
                case (int)Actions.ActionName.HideModal:
                    await AppContainer.HideModal();
                    break;
                case (int)Actions.ActionName.ToggleModal:
                    await AppContainer.ToggleModal();
                    break;
                case (int)Actions.ActionName.ShowForeground:
                    await AppContainer.ShowForeground();
                    break;
                case (int)Actions.ActionName.HideForeground:
                    await AppContainer.HideForeground();
                    break;
                case (int)Actions.ActionName.ToggleForeground:
                    await AppContainer.ToggleForeground();
                    break;
                case (int)Actions.ActionName.ShowPanel:
                    await AppContainer.ShowPanel();
                    break;
                case (int)Actions.ActionName.HidePanel:
                    await AppContainer.HidePanel();
                    break;
                case (int)Actions.ActionName.TogglePanel:
                    await AppContainer.TogglePanel();
                    break;

                case (int)Actions.ActionName.GoToStore:
                    await AppContainer.GoToNextPage();
                    break;
            }

            //await Task.Delay(1000);

            Console.WriteLine("Action: " + actionId + " completed");

            IsBusy = false;

            return true;
        }

        public static async Task<bool> ShowLoading()
        {
            await AppContainer.ShowLoading();
            return true;
        }

        public static async Task<bool> HideLoading()
        {
            await AppContainer.HideLoading();
            return true;
        }

        public static bool Update()
        {
            if (IsBusy) { return false; }
            return true;
        }

        public static async Task<bool> UpdatePage(int pageID)
        {
            await AppContainer.UpdatePage(pageID);
            return true;
        }

        public static async Task<bool> UpdatePage()
        {
            await AppContainer.UpdatePage();
            return true;
        }

        public static async Task<bool> ReloadPage(int pageID)
        {
            await AppContainer.ReloadPage(pageID);
            return true;
        }


        public static void ShowAlert(string text)
        {
            //appView.ShowAlert(text);
            DependencyService.Get<IMessage>().ShortAlert(text);
        }

        public static void ShowAlert(string title, string text)
        {
            //appView.ShowAlert(title, text);
            DependencyService.Get<IMessage>().ShortAlert(text);
        }


        public static void ShowModal()
        {
            AppContainer.ShowModal();
        }

        public static void HideModal()
        {
            AppContainer.HideModal();
        }

        /*
        public static async Task<bool> ShowAlert(string title, string text, string yes, string no)
        {
            return await AppContainer.ShowAlert(title, text, yes, no);
        }*/

        public async Task<bool> HandleHardwareBack()
        {
            Console.WriteLine("Hardware back pressed");
            if (Helpers.Pages.GetCurrent().Id != (int)AppSettings.PageNames.Landing)
            {
                await AppContainer.HardwareBack();
                return true;
            }
            return false; //;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            LocalDataStore.Save("app_start_time", DateTime.Now.ToString());
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            LocalDataStore.Save("app_sleep_time", DateTime.Now.ToString());
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            LocalDataStore.Save("app_resume_time", DateTime.Now.ToString());
        }

        // Data
        //public static string GetData(int requestId)
        //{
        //    return ApiBridge.GetResult(requestId).Result;
        //}

        // App Specific

    }
}
