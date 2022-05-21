using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Layouts.Custom;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class AccountCreated : Page
    {
        StackLayout ContentContainer;
        StackLayout MainLayout;

        StaticImage MainImage;
        StaticLabel CongratulationsLabel;
        StaticLabel UserLabel;
        StaticLabel CongratulationsInfo;
        StaticImage MainLogo;
        StaticImage BottomDecor;
        ColourButton ViewProfileButton;


        public AccountCreated()
        {
            this.IsScrollable = true;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.AccountCreated;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.White
            };

            AddBackgroundGradient(Colors.EC_GRADIENT_START, Colors.EC_GRADIENT_END);
            AddDecor("bottom_decor3.png", Units.ScreenHeight30Percent, LayoutOptions.EndAndExpand);
            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
        }


        public StackLayout BuildContent()
        {
            // build labels
            MainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.Transparent,// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                //HeightRequest = Units.ScreenHeight,
                //BackgroundColor = Color.Green,
            };

            BuildPageContent();

            return MainLayout;
        }

        private void BuildPageContent()
        {
            MainLayout.Children.Clear();

            /*
            MainLayout.Children.Add(BuildTopSection());
            MainLayout.Children.Add(BuildPersonalDetailsSection());
            MainLayout.Children.Add(BuildContactDetailsSection());
            MainLayout.Children.Add(BuildUserAccountSection());
            MainLayout.Children.Add(BuildDecorSection());
            MainLayout.Children.Add(BuildButtonSection());
            */
            //TopSectionLogo = new StaticImage("ecups_logo.png", 92, null);
            //TopSectionLogo.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            //TopSectionLogo.Content.Margin = new Thickness(Units.ScreenHeight5Percent, 40, Units.ScreenHeight5Percent, 0);

            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, 24);


            CongratulationsLabel = new StaticLabel("Congratulations");
            CongratulationsLabel.Content.FontFamily = Fonts.GetBoldFont();
            CongratulationsLabel.Content.FontSize = 48;
            CongratulationsLabel.Content.TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN);
            CongratulationsLabel.Content.HeightRequest = 64;
            CongratulationsLabel.Content.VerticalOptions = LayoutOptions.EndAndExpand;
            CongratulationsLabel.Content.VerticalTextAlignment = TextAlignment.End;
            CongratulationsLabel.CenterAlign();

            UserLabel = new StaticLabel("Mat");
            UserLabel.Content.FontFamily = Fonts.GetBoldFont();
            UserLabel.Content.FontSize = 48;
            UserLabel.Content.TextColor = Color.White;
            UserLabel.Content.HeightRequest = 64;
            UserLabel.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            UserLabel.Content.VerticalTextAlignment = TextAlignment.Start;
            UserLabel.CenterAlign();

            CongratulationsInfo = new StaticLabel("You are now a member of the Ecup team and it's now time to do your part in saving the planet.");
            CongratulationsInfo.Content.FontFamily = Fonts.GetBoldFont();
            CongratulationsInfo.Content.FontSize = Units.FontSizeL;
            CongratulationsInfo.Content.TextColor = Color.White;
            CongratulationsInfo.CenterAlign();

            MainImage = new StaticImage("octopus_with_cups.png", Units.ScreenWidth, null);
            MainImage.Content.HeightRequest = Units.ScreenWidth;
            MainImage.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            MainImage.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            MainImage.Content.Margin = Units.ScreenUnitM;

            ViewProfileButton = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "View Your Profile", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.YourAccount));
            ViewProfileButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;

            Grid BottomSection = new Grid
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                BackgroundColor = Color.LightGray,
                ColumnSpacing = 0,
                RowSpacing = 0
            };

            MainLayout.Children.Add(MainLogo.Content);

            MainLayout.Children.Add(CongratulationsLabel.Content);
            MainLayout.Children.Add(UserLabel.Content);

            MainLayout.Children.Add(CongratulationsInfo.Content);

            MainLayout.Children.Add(MainImage.Content);
            MainLayout.Children.Add(ViewProfileButton.Content);

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
                App.HideMenuButton();
            }
        }
    }
}