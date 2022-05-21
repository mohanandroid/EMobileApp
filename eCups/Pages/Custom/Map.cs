using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.e.CustomMapPin;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using eCups.Components.Maps;
using eCups.DatabaseObjects;
using eCups.DebugData.Custom;

namespace eCups.Pages.Custom
{
    public class Map : Page
    {
        StackLayout ContentContainer;
        Grid Header;
        StaticImage MainLogo;
        ColourButton BackButton;

        Label CafeName;
        Grid CafeInfo;
        Label A;
        Label Address;
        Label T;
        Label Telephone;
        Label W;
        Label Website;

        CustomMap map;
        Position currentUserPos;

        List<eCupPin> activePins;

        public Map()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.Map;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            
            map = new CustomMap
            {
                MapType = MapType.Street,
                HasScrollEnabled = true,
                HasZoomEnabled = true,
                IsShowingUser = true,
            };

            // add a background?
            //AddBackgroundImage("pagebg.jpg");
            //AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);
            AddBackgroundView(map, new Thickness(0,Units.ScreenHeight30Percent - Units.ScreenHeight2Percent,0,0)); //Margin to shrink map to not run past top border
            CenterMapToUser(map, 1);
            AddPinsToMap(DebugCafeList()); //Debug List of Cafes

            AddMenuBar();
            AddDecor("top_decor.png", Units.ScreenHeight40Percent, LayoutOptions.Start);

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
        }

        public override Task TransitionIn()
        {
            CenterMapToUser(map, 0.5f);
            return base.TransitionIn();
        }

        private async void CenterMapToUser(Xamarin.Forms.Maps.Map m, float mileRadius)
        {
            try
            {
                Location userLocation = await Geolocation.GetLastKnownLocationAsync();
                AppSettings.userLocation = userLocation;
                m.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(userLocation.Latitude, userLocation.Longitude), Distance.FromMiles(mileRadius)));
                currentUserPos = new Position(userLocation.Latitude, userLocation.Latitude);
            }
            catch
            {
                //give backup location
                currentUserPos = new Position(0, 0);
            }
            
        }

        private void AddPinsToMap(eCupPin[] cafes, float mileRadius = 0)
        {
            activePins = new List<eCupPin>();
            foreach (eCupPin cafe in cafes)
            {
                if(mileRadius == 0)
                {
                    map.Pins.Add(cafe);
                    activePins.Add(cafe);
                }
                else
                {
                    //check distance from user or search location
                    if(Location.CalculateDistance(currentUserPos.Latitude, currentUserPos.Longitude, cafe.Position.Latitude, cafe.Position.Longitude, DistanceUnits.Miles) <= mileRadius)
                    {
                        map.Pins.Add(cafe);
                        activePins.Add(cafe);
                    }
                }
            }

            if(activePins.Count == 0 || activePins == null)
            {
                //no results found 
            }
            else
            {
                map.CustomPins = activePins;
            }
        }

        public StackLayout BuildContent()
        {
            // build labels
            StackLayout mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight30Percent //height reduced on overlay content to not block the background view
            };

            Header = new Grid
            {
                ColumnDefinitions = 
                {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };

            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth30Percent, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenWidth5Percent, Units.ScreenHeight5Percent, 0, 0);
            MainLogo.Content.HorizontalOptions = LayoutOptions.Start;

            BackButton = new ColourButton(Color.FromHex(Colors.EC_GREEN_2), Color.White, "Back <", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Menu));
            BackButton.Content.VerticalOptions = LayoutOptions.End;
            BackButton.Content.HorizontalOptions = LayoutOptions.End;
            BackButton.SetSize(100, 40);
            BackButton.Content.Margin = new Thickness(Units.ScreenWidth5Percent, 0);

            Header.Children.Add(MainLogo.Content, 0, 0);
            Header.Children.Add(BackButton.Content, 1, 0);

            CafeName = new Label
            {
                Text = null,
                FontSize = 32,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Start
            };

            CafeName.Margin = new Thickness(Units.ScreenWidth5Percent, Units.ScreenHeight2Percent);

            CafeInfo = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = Units.ScreenWidth5Percent },
                    new ColumnDefinition()
                }
            };

            A = new Label
            {
                Text = "A:",
                FontSize = 12,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                HorizontalOptions = LayoutOptions.Start
            };

            T = new Label
            {
                Text = "T:",
                FontSize = 12,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                HorizontalOptions = LayoutOptions.Start
            };

            W = new Label
            {
                Text = "W:",
                FontSize = 12,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                HorizontalOptions = LayoutOptions.Start
            };

            Address = new Label
            {
                Text = "Placeholder Address",
                FontSize = 12,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Telephone = new Label
            {
                Text = "01482 000 000",
                FontSize = 12,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Website = new Label
            {
                Text = "website-address.co.uk",
                FontSize = 12,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            var linkToWebsite = new TapGestureRecognizer();
            linkToWebsite.Tapped += (s, e) =>
            {
                Label label = (Label)s;
                if(AppSettings.openLinksInApp)
                {
                    throw new Exception("Not Implemented");
                }
                else
                {
                    Browser.OpenAsync(label.Text);
                }
            };

            Website.GestureRecognizers.Add(linkToWebsite);

            CafeInfo.Children.Add(A, 0, 0);
            CafeInfo.Children.Add(Address, 1, 0);
            CafeInfo.Children.Add(T, 0, 1);
            CafeInfo.Children.Add(Telephone, 1, 1);
            CafeInfo.Children.Add(W, 0, 2);
            CafeInfo.Children.Add(Website, 1, 2);

            CafeInfo.Margin = new Thickness(Units.ScreenWidth5Percent, Units.ScreenHeight2Percent);


            /*mainLayout.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Landing);
                           });
                       })
                   }
               );*/

            
            mainLayout.Children.Add(Header);
            mainLayout.Children.Add(CafeName);
            mainLayout.Children.Add(CafeInfo);
            //mainLayout.Children.Add(map);
            //mainLayout.Children.Add(new Label { Text = "Map" });
            return mainLayout;
        }

        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);

                this.HasHeader = false;
                this.HasFooter = true;
                await base.Update();

                PageContent.Children.Remove(ContentContainer);
                ContentContainer = BuildContent();
                PageContent.Children.Add(ContentContainer);

                AddMenuBar();
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }

        public void UpdateInfo(eCupPin pin)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                CafeName.Text = pin.Name;
                Address.Text = pin.Address;
                Telephone.Text = pin.Tel;
                Website.Text = pin.URL;
            });
            Console.WriteLine("Triggered Map Info Update");
        }

        private eCupPin[] DebugCafeList() //these are all centric to newland area, please look there if you cant find any on debug
        {
            eCupPin[] cafes = new eCupPin[FakeData.DebugCafeList.Length];
            foreach(CafeObject entry in FakeData.DebugCafeList)
            {
                cafes[entry.ID] = new eCupPin
                {
                    Name = entry.Name,
                    Label = entry.Name,
                    Address = entry.Address.AddressLine1,
                    Position = entry.Coords,
                    URL = entry.URL,
                    Tel = entry.Phone,
                    page = this
                };
            }
            return cafes;
        }
    }
}