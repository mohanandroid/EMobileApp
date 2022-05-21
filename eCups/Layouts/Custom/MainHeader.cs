using System;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Layouts.Custom
{
    public class MainHeader
    {
        public Grid Content;
        ActiveImage Logo;
        IconButton MenuButton;

        public MainHeader()
        {
            Content = new Grid
            {
                HeightRequest = Units.ScreenHeight15Percent,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(Units.ScreenHeight5Percent, 0, Units.ScreenWidth10Percent, 1),
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.FromHex(Colors.BH_DARK_BLUE)
            };

            Logo = new ActiveImage("logo.png", (int)(Units.ScreenWidth * 0.65), Units.HalfScreenWidth, null, null);

            /*Logo.Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               if (AppSession.CurrentUser.IsRegistered)
                               {
                                   await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.TELandingPage);
                               }
                               else
                               {
                                   App.ShowAlert("Please register to access the rest of the app");
                               }
                           });
                       })
                   }
               );*/


            Logo.Content.VerticalOptions = LayoutOptions.End;
            Logo.Content.HorizontalOptions = LayoutOptions.Start;
            Logo.Content.BackgroundColor = Color.Transparent;
            Logo.Image.Aspect = Aspect.AspectFit;

            MenuButton = new IconButton(Units.TapSizeL, Units.TapSizeXL, Color.Transparent, Color.Transparent, "", "menu_box.png", null);

            MenuButton.Content.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                /*
                                if (AppSession.CurrentUser.IsRegistered)
                                {
                                    await App.ShowMenu();
                                }
                                else
                                {
                                    App.ShowAlert("Please register to access the rest of the app");
                                }*/
                                await App.ShowMenu();
                            });
                        })
                    }
                );

            MenuButton.Content.BackgroundColor = Color.Transparent;
            MenuButton.Content.HorizontalOptions = LayoutOptions.End;
            MenuButton.Content.VerticalOptions = LayoutOptions.CenterAndExpand;

            MenuButton.Icon.Content.VerticalOptions = LayoutOptions.CenterAndExpand;
            MenuButton.Icon.Content.HorizontalOptions = LayoutOptions.End;
            MenuButton.Icon.Content.Aspect = Aspect.AspectFill;


            Content.Children.Add(Logo.Content, 0, 0);

            Content.Children.Add(MenuButton.Content, 2, 0);
            Grid.SetColumnSpan(Logo.Content, 2);

            SetLargeIcon();
            HideMenuButton();
        }

        public void HideMenuButton()
        {
            Logo.Content.HorizontalOptions = LayoutOptions.Center;
            Content.Padding = Units.ScreenWidth10Percent;// new Thickness(Units.ScreenWidth10Percent, Units.ScreenWidth10Percent, Units.ScreenWidth10Percent, 1);
            Content.Children.Clear();
            Content.Children.Add(Logo.Content, 0, 0);
            //Content.Children.Add(MenuButton.Content, 2, 0);
            //Grid.SetColumnSpan(Logo.Content, 2);
        }

        public void ShowMenuButton()
        {
            Logo.Content.HorizontalOptions = LayoutOptions.Start;
            Content.Padding = new Thickness(Units.ScreenHeight5Percent, 0, Units.ScreenWidth10Percent, 1);
            Content.Children.Clear();
            Content.Children.Add(Logo.Content, 0, 0);
            Content.Children.Add(MenuButton.Content, 2, 0);
            Grid.SetColumnSpan(Logo.Content, 2);
        }

        public void SetLargeIcon()
        {
            Content.Opacity = 1;
        }

        public void SetSmallIcon()
        {
            Content.Opacity = 0;
        }
    }
}
