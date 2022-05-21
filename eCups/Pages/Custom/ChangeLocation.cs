using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Fields;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class ChangeLocation : Page
    {
        StackLayout ContentContainer;
        StaticImage MainLogo;
        Grid Heading;
        Grid BackgroundGrid;

        public ChangeLocation()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.ChangeLocation;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.Transparent
            };

            BackgroundGrid = new Grid
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = Units.ScreenHeight - Units.ScreenHeight20Percent,
                Padding = new Thickness(Units.ScreenWidth10Percent, 0),
                RowSpacing = 30,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                }
            };

            PopulateBG();

            AddBackgroundView(BackgroundGrid);

            AddDecor("top_decor.png", Units.ScreenHeight30Percent, LayoutOptions.Start);

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
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
                HeightRequest = Units.ScreenHeight20Percent
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
                Text = "Change Location"
            };

            Label Subtitle = new Label
            {
                TextColor = Color.White,
                Text = "Change your location below by location or via name or postcode.",
                FontSize = 12,
                FontFamily = Fonts.GetRegularFont()
            };

            Heading.Children.Add(Title, 0, 0);
            Heading.Children.Add(Subtitle, 0, 1);

            mainLayout.Children.Add(MainLogo.Content);
            mainLayout.Children.Add(Heading);

            AddMenuBar();
            return mainLayout;
        }

        void PopulateBG()
        {
            BorderInputField City = new BorderInputField("City", Keyboard.Text, false);
            BorderInputField PostCode = new BorderInputField("Postcode", Keyboard.Text, false);

            ColourButton Change = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "Change Location", new Models.Action((int)Helpers.Actions.ActionName.GoToLastPage));

            City.GetContent().HorizontalOptions = LayoutOptions.Center;
            City.GetContent().VerticalOptions = LayoutOptions.End;
            PostCode.GetContent().HorizontalOptions = LayoutOptions.Center;
            PostCode.GetContent().VerticalOptions = LayoutOptions.Center;
            Change.GetContent().HorizontalOptions = LayoutOptions.Start;
            Change.GetContent().VerticalOptions = LayoutOptions.Start;
            Change.GetContent().WidthRequest = 200;

            Change.GetContent().GestureRecognizers.Add(
               new TapGestureRecognizer()
               {
                   Command = new Command(() =>
                   {
                       Device.BeginInvokeOnMainThread(async () =>
                       {
                          //Api Call to update user location
                       });
                   })
               }
           );

            BackgroundGrid.Children.Add(City.GetContent(), 0, 0);
            BackgroundGrid.Children.Add(PostCode.GetContent(), 0, 1);
            BackgroundGrid.Children.Add(Change.GetContent(), 0, 2);
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
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }
    }
}
