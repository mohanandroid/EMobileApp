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
    public class NewLevel : Page
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


        public NewLevel()
        {
            this.IsScrollable = false;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.NewLevel;
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


            MainLogo = new StaticImage("ecups_logo.png", Units.ScreenWidth, null);
            MainLogo.Content.Margin = new Thickness(Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, Units.ScreenHeight10Percent, 24);


            CongratulationsLabel = new StaticLabel("Congratulations");
            CongratulationsLabel.Content.FontFamily = Fonts.GetBoldFont();
            CongratulationsLabel.Content.FontSize = Units.FontSizeXXXL;
            CongratulationsLabel.Content.TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN);
            CongratulationsLabel.Content.VerticalOptions = LayoutOptions.EndAndExpand;
            CongratulationsLabel.Content.VerticalTextAlignment = TextAlignment.End;
            CongratulationsLabel.CenterAlign();

            UserLabel = new StaticLabel("You have reached a new level");
            UserLabel.Content.FontFamily = Fonts.GetBoldFont();
            UserLabel.Content.FontSize = Units.FontSizeXL;
            UserLabel.Content.TextColor = Color.White;

            UserLabel.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            UserLabel.Content.VerticalTextAlignment = TextAlignment.Start;
            UserLabel.CenterAlign();

            /*
            CongratulationsInfo = new StaticLabel("You are now a member of the Ecup team and it's now time to do your part in saving the planet.");
            CongratulationsInfo.Content.FontFamily = Fonts.GetBoldFont();
            CongratulationsInfo.Content.FontSize = Units.FontSizeL;
            CongratulationsInfo.Content.TextColor = Color.White;
            CongratulationsInfo.CenterAlign();
            */


            MainImage = new StaticImage("jellyfish.png", 128, null);
            MainImage.Content.HeightRequest = Units.ScreenWidth;
            MainImage.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            MainImage.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            MainImage.Content.Margin = Units.ScreenUnitM;

            StaticLabel LevelUpName = new StaticLabel("Jellyfish");
            LevelUpName.Content.FontFamily = Fonts.GetBoldFont();
            LevelUpName.Content.FontSize = Units.FontSizeXXXL;
            LevelUpName.Content.TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN);
            LevelUpName.RightAlign();

            StaticLabel LevelUpNumber = new StaticLabel("- Level 3");
            LevelUpNumber.Content.FontFamily = Fonts.GetBoldFont();
            LevelUpNumber.Content.FontSize = Units.FontSizeXXXL;
            LevelUpNumber.Content.TextColor = Color.White;
            LevelUpNumber.LeftAlign();

            StaticLabel DiscountName = new StaticLabel("This level gives you 7% off your order");
            DiscountName.Content.FontFamily = Fonts.GetBoldFont();
            DiscountName.Content.FontSize = Units.FontSizeXL;
            DiscountName.Content.TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN);
            DiscountName.LeftAlign();

            StaticLabel DiscountDetail = new StaticLabel("This means that if your spend, like.. ten quid.. it will only cost you £9.70. Nice!");
            DiscountDetail.Content.FontFamily = Fonts.GetBoldFont();
            DiscountDetail.Content.FontSize = Units.FontSizeL;
            DiscountDetail.Content.TextColor = Color.White;
            DiscountDetail.LeftAlign();



            Grid NewLevelInfoLayout = new Grid
            {
                Margin = new Thickness(Units.ScreenUnitM, Units.ScreenUnitM, Units.ScreenUnitM, Units.ScreenHeight20Percent),
                HeightRequest = Units.ScreenWidth * 0.8,
                VerticalOptions = LayoutOptions.EndAndExpand

            };



            NewLevelInfoLayout.Children.Add(LevelUpName.Content, 0, 0);
            NewLevelInfoLayout.Children.Add(LevelUpNumber.Content, 1, 0);
            NewLevelInfoLayout.Children.Add(MainImage.Content, 0, 1);
            NewLevelInfoLayout.Children.Add(DiscountName.Content, 1, 1);
            NewLevelInfoLayout.Children.Add(DiscountDetail.Content, 1, 2);

            Grid.SetRowSpan(MainImage.Content, 2);


            ViewProfileButton = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "View Your Profile", null);
            ViewProfileButton.Content.VerticalOptions = LayoutOptions.EndAndExpand;




            MainLayout.Children.Add(MainLogo.Content);

            MainLayout.Children.Add(CongratulationsLabel.Content);
            MainLayout.Children.Add(UserLabel.Content);

            //MainLayout.Children.Add(CongratulationsInfo.Content);

            MainLayout.Children.Add(NewLevelInfoLayout);
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