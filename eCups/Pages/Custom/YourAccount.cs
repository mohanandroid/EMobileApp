using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.Components.Composites;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Helpers.Custom;
using eCups.Layouts.Custom;
using eCups.Models.Custom;
using eCups.Services.Storage;
using Xamarin.Forms;
using static eCups.Models.User;

namespace eCups.Pages.Custom
{
    public class YourAccount : Page
    {
        StackLayout ContentContainer;
        StaticImage MainLogo;
        Grid Heading;

        public YourAccount()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.YourAccount;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.White
            };


            /*CScrollView scroll = new CScrollView
            {
                BackgroundColor = Color.White,
                Orientation = ScrollOrientation.Vertical,
                Content = AccountScrollContent(),
                Elastic = false,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Units.ScreenWidth,
                //HeightRequest = Units.ScreenHeight - Units.ScreenHeight25Percent,
                Margin = new Thickness(0, 0, 0, 0),
            };

            // add a background?
            AddBackgroundView(scroll);*/
            AddDecor("top_decor.png", Units.ScreenHeight30Percent, LayoutOptions.Start);

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);


        }

        public StackLayout AccountScrollContent()
        {
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0, 0, 0, 0)
            };

            if (!String.IsNullOrEmpty(LocalDataStore.Load("Cupscount")))
            {
                stack.Children.Add(Title());
                //TODO - Loop foreach cup setting last to true
                foreach (CupsHolding cups in AppSession.CurrentUser.details.cups_holding)
                {
                    stack.Children.Add(MyCupGrid(cups, true));

                }
                stack.Children.Add(AddCupButton());

            }

            try
            {
                stack.Children.Add(UserInfo());
            }
            catch
            {
                Console.WriteLine("Error with user info");
            }

            return stack;
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
                MinimumHeightRequest = Units.ScreenHeight20Percent
            };

            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth30Percent, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenWidth5Percent, Units.ScreenHeight5Percent, 0, 0);
            MainLogo.Content.HorizontalOptions = LayoutOptions.Center;

            Heading = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = Units.ScreenWidth - Units.ScreenWidth40Percent},
                    new ColumnDefinition(),
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = 40, },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
                Margin = new Thickness(20, 20, 20, 0)

            };

            Label Title = new Label
            {
                TextColor = Color.White,
                FontSize = 24,
                FontFamily = Fonts.GetBoldFont(),
                Text = "Your Account"
            };

            Heading.Children.Add(Title, 0, 0);

            mainLayout.Children.Add(MainLogo.Content);
            mainLayout.Children.Add(Heading);
            CScrollView scroll = new CScrollView
            {
                BackgroundColor = Color.White,
                Orientation = ScrollOrientation.Vertical,
                Content = AccountScrollContent(),
                Elastic = false,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Units.ScreenWidth,
                //HeightRequest = Units.ScreenHeight - Units.ScreenHeight25Percent,
                Margin = new Thickness(0, 0, 0, 0),
            };

            // add a background?
            //AddBackgroundView(scroll);
            mainLayout.Children.Add(scroll);
            return mainLayout;
        }

        Grid MyCupGrid(CupsHolding cup, bool addCupButton)
        {


            Grid content = new Grid
            {
                Margin = new Thickness(5, 5, 5, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 10,
                RowSpacing = 15,
                ColumnDefinitions =
                {
                      new ColumnDefinition { Width = new GridLength(90, GridUnitType.Absolute)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto)}
                },
                RowDefinitions =
                {
                   new RowDefinition { Height = new GridLength(25, GridUnitType.Absolute)},
                    new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute)},
                    new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}
                }

            };


            Label header = new Label
            {
                Text = "Your Ecup:",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                IsVisible = false,
                TextColor = Color.FromHex(Colors.EC_GREEN_1),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };

            Image cupImage = new Image
            {
                Source = "cup.png",
                Aspect = Aspect.AspectFit,
                HeightRequest = 100,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout serial = CupInfo("Ecup Serial No:", cup.ecups_serial_no);
            StackLayout date = CupInfo("Registered Date", cup.created_at.ToString());


            content.Children.Add(header, 0, 0);
            Grid.SetColumnSpan(header, content.ColumnDefinitions.Count);
            content.Children.Add(cupImage, 0, 1);
            Grid.SetRowSpan(cupImage, content.RowDefinitions.Count - 2);
            content.Children.Add(serial, 1, 1);
            content.Children.Add(date, 1, 2);

            /*if (addCupButton)
            {
                ColourButton addNewCup = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "Add New Cup", new Models.Action((int)Helpers.Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner));

                addNewCup.SetSize(150, 25);
                addNewCup.SetFontSize(10);
                addNewCup.Content.HorizontalOptions = LayoutOptions.Start;

                content.Children.Add(addNewCup.GetContent(), 0, 4);
                Grid.SetColumnSpan(addNewCup.GetContent(), content.ColumnDefinitions.Count);
            }*/


            return content;

        }

        Label Title()
        {

            Label title = new Label
            {
                Text = "Your Ecup:",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_GREEN_1),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };

            return title;
        }
        StackLayout AddCupButton()
        {
            StackLayout infoStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                Spacing = 10,
                WidthRequest = Units.ScreenWidth,
                Margin = new Thickness(0, 0, 0, 50)
            };
            ColourButton addNewCup = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "Add New Cup", new Models.Action((int)Helpers.Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner));

            addNewCup.SetSize(150, 25);
            addNewCup.SetFontSize(10);
            addNewCup.Content.HorizontalOptions = LayoutOptions.Start;

            infoStack.Children.Add(addNewCup.GetContent());
            return infoStack;
        }



        StackLayout UserInfo()
        {
            StackLayout infoStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                Spacing = 10,
                WidthRequest = Units.ScreenWidth,
                Margin = new Thickness(0, 0, 0, 50)
            };

            Label detailsHeader = new Label
            {
                Text = "Your Details",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_GREEN_1),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10)
            };

            LabelEntry name = new LabelEntry("Name:", AppSession.CurrentUserDetails.name, false, true);
            LabelEntry email = new LabelEntry("Email:", AppSession.CurrentUserDetails.email, false, true);

            LabelEntry user = new LabelEntry("User:", LocalDataStore.Load("Username"), false, true);


            LabelEntry password = new LabelEntry("Password:", LocalDataStore.Load("Password"), false, true);
            // LabelEntry confirmPassword = new LabelEntry("Confirm Password:", LocalDataStore.Load("PASSWORD"), false, true);

            password.Content.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        password.EntryLabel.SetPassword(!password.passDisplay);
                        password.passDisplay = !password.passDisplay;
                    });
                })
            });

            Label contactDetails = new Label
            {
                Text = "Contact Details",
                FontFamily = Fonts.GetBoldFont(),
                FontSize = 16,
                TextColor = Color.FromHex(Colors.EC_GREEN_1),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10)
            };

            //LabelEntry address = new LabelEntry("Address:", AppSession.CurrentUserDetails.address3.FormattedAddress(), false);
            LabelEntry address = new LabelEntry("Address:", AppSession.CurrentUserDetails.address2, false, true);
            LabelEntry contact = new LabelEntry("Contact:", AppSession.CurrentUserDetails.phone.ToString(), false, true);

            infoStack.Children.Add(detailsHeader);
            infoStack.Children.Add(name.Content);
            infoStack.Children.Add(email.Content);
            infoStack.Children.Add(user.Content);
            infoStack.Children.Add(password.Content);
            // infoStack.Children.Add(confirmPassword.Content);
            infoStack.Children.Add(contactDetails);
            infoStack.Children.Add(address.Content);
            infoStack.Children.Add(contact.Content);

            return infoStack;
        }



        StackLayout CupInfo(string headerText, string subHeaderText)
        {

            StackLayout infoStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
            };

            Label header = new Label
            {
                Text = headerText,
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 14,
                TextColor = Color.FromHex(Colors.EC_GREEN_1),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
            };

            Label subHeader = new Label
            {
                Text = subHeaderText,
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 12,
                TextColor = Color.FromHex(Colors.EC_GREEN_2),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
            };

            infoStack.Children.Add(header);
            infoStack.Children.Add(subHeader);

            return infoStack;
        }

        public override Task TransitionIn()
        {
            NeedsRefreshing = true;
            return base.TransitionIn();
        }

        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);

                bool value = await App.ApiBridge.GetUser();
                await App.HideLoading();


                this.HasHeader = false;
                this.HasFooter = true;
                await base.Update();
                if (value)
                {
                    PageContent.Children.Remove(ContentContainer);
                    ContentContainer = BuildContent();
                    PageContent.Children.Add(ContentContainer);
                }

                AddMenuBar();
                //App.ShowMenuButton();
                // App.HideMenuButton();
                NeedsRefreshing = false;
            }
        }


    }
}