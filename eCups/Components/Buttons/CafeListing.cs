using System;
using eCups.Helpers;
using eCups.Tools;
using Xamarin.Forms;
using XFShapeView;
using eCups.e.Buttons;
using eCups.DatabaseObjects;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace eCups.e.CafeListing
{
    public class CafeListing : e.ActiveComponent
    {
        public StackLayout Listing;
        public Grid Buttons;
        public Label Title;
        public Label Para;
        public Label Distance;
        public ColourButton Locate;
        public ColourButton View;
        public float minutesFrom;

        public CafeListing(bool altBg)
        {
            if (!altBg)
            {

                this.Content = new Grid
                {
                    BackgroundColor = Color.White,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 0, -6),
                };
            }
            else
            {

                this.Content = new Grid
                {
                    BackgroundColor = Color.FromHex("#F8F8F8"),
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 0, -6),
                };
            }

            Listing = new StackLayout
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight15Percent,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(20, 0, 20, 0)
            };

            this.Content.Children.Add(Listing);
        }

        public CafeListing(CafeObject cafe)
        {

            this.Content = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, -6),
            };


            Listing = new StackLayout
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight35Percent,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(20, 0, 20, 0)
            };

            Buttons = new Grid
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.End,
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
                Margin = new Thickness(0, 0, 40, 20)
                
            };

            Title = new Label
            {
                TextColor = Color.Black,
                FontSize = 24,
                Text = cafe.Name,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = Fonts.GetBoldFont(),
                Margin = new Thickness(0, 10, 0, 0)
            };

            Para = new Label
            {
                TextColor = Color.Black,
                Text = cafe.Description,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = Fonts.GetBoldFont(),
                Margin = new Thickness(0, 20, 0, 0)
            };

            if(AppSettings.userLocation == null)
            {
                AppSettings.userLocation = new Location(0, 0);
            }

            float dist = (float)Location.CalculateDistance(AppSettings.userLocation.Latitude, AppSettings.userLocation.Longitude, cafe.Coords.Latitude, cafe.Coords.Longitude, DistanceUnits.Miles);
            minutesFrom = (dist / 3) * 60;

            Distance = new Label
            {
                TextColor = Color.FromHex("#119DA4"),
                Text = Math.Ceiling(minutesFrom) + " minutes from your location",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.EndAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = Fonts.GetBoldFont(),
                Margin = new Thickness(0, 0, 0, 20)
            };

            Locate = new ColourButton(Color.FromHex("#011627"), Color.White, "Locate", new Models.Action((int)Helpers.Actions.ActionName.OpenMap, ("daddr=" + AppSettings.userLocation.Latitude.ToString() + "," + AppSettings.userLocation.Longitude.ToString() + "&saddr=" + cafe.Coords.Latitude.ToString() + "," + cafe.Coords.Longitude.ToString()))); //need to add map center to location call
            View = new ColourButton(Color.FromHex("#0C7489"), Color.White, "View Store", new Models.Action((int)Helpers.Actions.ActionName.GoToStore, cafe.ID)); //need to add launch cafe page call 

            Locate.SetSize(200, 30);
            View.SetSize(200, 30);

            if (this.DefaultAction != null)
            {
                this.Content.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await this.DefaultAction.Execute();
                            });
                        })
                    }
                );
            }

            Listing.Children.Add(Title);
            Listing.Children.Add(Para);
            Listing.Children.Add(Distance);
            Buttons.Children.Add(Locate.Content, 0, 0);
            Buttons.Children.Add(View.Content, 1, 0);
            Listing.Children.Add(Buttons);
            this.Content.Children.Add(Listing);
        }

        public Grid ReturnItem(bool altBg)
        {
            if (altBg)
            {
                this.Content.BackgroundColor = Color.FromHex("#F8F8F8");
            }
            return this.Content;
        }
    }
}

