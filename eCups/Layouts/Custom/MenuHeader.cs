using System;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Layouts.Custom
{
    public class MenuHeader
    {
        public Grid Content;
        ActiveImage Logo;
        IconButton MenuButton;

        //Image RightDots;

        public MenuHeader()
        {
            Content = new Grid
            {
                HeightRequest = Units.ScreenHeight15Percent,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(Units.ScreenHeight5Percent, 0, Units.ScreenWidth10Percent, 1),
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.Transparent
            };

            Logo = new ActiveImage("ecups_logo.png", (int)(Units.ScreenWidth * 0.45), Units.HalfScreenWidth, null, null);
            Logo.Content.VerticalOptions = LayoutOptions.End;
            Logo.Content.HorizontalOptions = LayoutOptions.Start;
            Logo.Content.BackgroundColor = Color.Transparent;
            Logo.Image.Aspect = Aspect.AspectFit;


            //MenuButton = new IconButton(Units.TapSizeM, Units.TapSizeM, Color.Transparent, Color.Transparent, "", "menucloseicon.png", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.TEMenuPage));
            MenuButton = new IconButton(28, 28, Color.Transparent, Color.Transparent, "", "close.png", null);

            MenuButton.Content.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                
                                await App.CloseMenu();

                            });
                        })
                    }
                );

            /*MenuButton.Content.BackgroundColor = Color.Transparent;
            MenuButton.Content.HorizontalOptions = LayoutOptions.End;
            MenuButton.Content.VerticalOptions = LayoutOptions.Center;

            MenuButton.Icon.Content.Margin = new Thickness(0, Units.ScreenUnitXS);
            MenuButton.Icon.Content.VerticalOptions = LayoutOptions.Center;
            MenuButton.Icon.Content.HorizontalOptions = LayoutOptions.End;*/
            MenuButton.Content.BackgroundColor = Color.Transparent;
            MenuButton.Content.HorizontalOptions = LayoutOptions.End;
            MenuButton.Content.VerticalOptions = LayoutOptions.CenterAndExpand;


            //MenuButton.Icon.Content.Margin = new Thickness(0, Units.ScreenUnitXS);
            MenuButton.Icon.Content.VerticalOptions = LayoutOptions.CenterAndExpand;
            MenuButton.Icon.Content.HorizontalOptions = LayoutOptions.End;
            MenuButton.Icon.Content.Aspect = Aspect.AspectFill;

            //RightDots = new Image { Source = "dotssquares.png" };

            Content.Children.Add(Logo.Content, 0, 0);
            Content.Children.Add(MenuButton.Content, 2, 0);
            //Content.Children.Add(RightDots, 2, 0);

            Grid.SetColumnSpan(Logo.Content, 2);

            //SetSmallIcon();
            //SetLargeIcon();

        }

        public void SetLargeIcon()
        {
            Content.Opacity = 1;

            //Logo.Content.WidthRequest = Units.ScreenWidth;
            //Logo.Image.WidthRequest = Units.ScreenWidth;

            //Logo.Content.Padding = new Thickness(0, Units.ScreenUnitM, Units.ScreenUnitL, Units.ScreenUnitM);
            //Logo.Image.Margin = new Thickness(0, Units.ScreenUnitM);
        }

        public void SetSmallIcon()
        {
            Content.Opacity = 0;

            //Logo.Content.WidthRequest = Units.ScreenWidth;
            //Logo.Image.WidthRequest = Units.ScreenWidth;

            //Logo.Content.Padding = new Thickness(0, Units.ScreenUnitS, Units.ScreenWidth20Percent, Units.ScreenUnitM);
            //Logo.Image.Margin = new Thickness(0, Units.ScreenUnitM);
        }
    }
}
