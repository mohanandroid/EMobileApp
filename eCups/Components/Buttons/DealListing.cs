using System;
using eCups.e.Buttons;
using eCups.Helpers;
using eCups.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using eCups.Branding;

namespace eCups.Components.Buttons
{
    public class DealListing : e.ActiveComponent
    {
        public StackLayout Listing;
        public Grid Buttons;
        public Label Title;
        public Label Para;
        public Label Distance;
        public ColourButton Locate;
        public ColourButton View;
        public float minutesFrom;

        public DealListing(DealObject deal)
        {
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
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(20, 0, 20, 0)
                };

                Buttons = new Grid
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.Start,
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
                    FontSize = 14,
                    Text = deal.Name,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontFamily = Fonts.GetBoldFont(),
                    Margin = new Thickness(0, 10, 0, 0)
                };

                Para = new Label
                {
                    TextColor = Color.FromHex(Colors.EC_MID_GREEN),
                    Text = deal.Para,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontFamily = Fonts.GetRegularFont(),
                };

                if (AppSettings.userLocation == null)
                {
                    AppSettings.userLocation = new Xamarin.Essentials.Location(0, 0);
                }

                float dist = (float)Xamarin.Essentials.Location.CalculateDistance(AppSettings.userLocation.Latitude, AppSettings.userLocation.Longitude, deal.Coords.Latitude, deal.Coords.Longitude, DistanceUnits.Miles);
                minutesFrom = (dist / 3) * 60;

                Distance = new Label
                {
                    TextColor = Color.FromHex(Colors.EC_GREEN_2),
                    Text = Math.Ceiling(minutesFrom) + " minutes from your location",
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontFamily = Fonts.GetRegularFont(),
                };

                Locate = new ColourButton(Color.FromHex(Colors.EC_GREEN_1), Color.White, "Locate", new Models.Action((int)Helpers.Actions.ActionName.OpenMap, ("daddr=" + AppSettings.userLocation.Latitude.ToString() + "," + AppSettings.userLocation.Longitude.ToString() + "&saddr=" + deal.Coords.Latitude.ToString() + "," + deal.Coords.Longitude.ToString()))); //need to add map center to location call
                View = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "View Store", new Models.Action((int)Helpers.Actions.ActionName.GoToStore, deal.ID)); //need to add launch cafe page call 

                Locate.SetSize(70, 25);
                Locate.SetFontSize(10);
                View.SetSize(70, 25);
                View.SetFontSize(10);

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
