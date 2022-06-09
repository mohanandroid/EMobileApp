using System;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Pages.Custom
{
    public class EmailVerficiation : Page
    {
        StackLayout ContentContainer;
        StackLayout MainLayout;
        StaticImage TopSectionBackground;
        StaticImage TopSectionLogo;
        StaticLabel TopSectionSectionTitle;
        Grid TopSection;
        Grid EmailDetailsSection;

        public EmailVerficiation()
        {

            this.IsScrollable = true;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.EmailVerficiation;
            this.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;
            this.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;

            PageContent = new Grid
            {
                BackgroundColor = Color.White
            };

            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
        }

        public StackLayout BuildContent()
        {
            // build labels
            MainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.FromHex(Colors.EC_WHITE),
                WidthRequest = Units.ScreenWidth,
                HeightRequest = Units.ScreenHeight,
                Spacing = 0
            };

            MainLayout.Children.Add(BuildTopSection());
            MainLayout.Children.Add(BuildEmailDetailsSection());
            return MainLayout;
        }

        private Grid BuildTopSection()
        {
            TopSection = new Grid
            {
                HeightRequest = Units.ScreenHeight40Percent,
                BackgroundColor = Color.FromHex(Colors.EC_WHITE)
            };
            StackLayout TopSectionContent = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(Units.ScreenWidth5Percent, 0, Units.ScreenWidth5Percent, Units.ScreenHeight15Percent)
            };


            TopSectionBackground = new StaticImage("top_decor.png", Units.ScreenWidth, null);
            TopSectionBackground.Content.Aspect = Aspect.Fill;

            TopSectionLogo = new StaticImage("ecups_logo.png", 92, null);
            TopSectionLogo.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            TopSectionLogo.Content.Margin = new Thickness(Units.ScreenHeight5Percent, 40, Units.ScreenHeight5Percent, 0);

            TopSectionSectionTitle = new StaticLabel("Email Verficiation");
            TopSectionSectionTitle.Content.TextColor = Color.White;
            TopSectionSectionTitle.Content.FontSize = Units.FontSizeXXL;
            TopSectionSectionTitle.Content.FontFamily = Fonts.GetBoldFont();
            TopSectionSectionTitle.CenterAlign();
            TopSectionSectionTitle.Content.Margin = new Thickness(Units.ScreenHeight5Percent, 0, Units.ScreenHeight5Percent, 0);
            TopSectionSectionTitle.Content.VerticalOptions = LayoutOptions.StartAndExpand;

            TopSectionContent.Children.Add(TopSectionLogo.Content);
            TopSectionContent.Children.Add(TopSectionSectionTitle.Content);
            TopSection.Children.Add(TopSectionBackground.Content, 0, 0);
            TopSection.Children.Add(TopSectionContent, 0, 0);

            return TopSection;
        }


        private Grid BuildEmailDetailsSection()
        {
            StaticLabel ConfirmEmailLabel = new StaticLabel("Confirm Your Email Address");
            ConfirmEmailLabel.Content.FontSize = Units.FontSizeXL;
            ConfirmEmailLabel.Content.FontFamily = Fonts.GetBoldFont();
            ConfirmEmailLabel.CenterAlign();

            StaticImage EmailImage = new StaticImage("account.png", 75, 75, null);
            EmailImage.Content.Aspect = Aspect.AspectFit;
            EmailImage.Content.VerticalOptions = LayoutOptions.Center;
            EmailImage.Content.HorizontalOptions = LayoutOptions.Center;

            StaticLabel EmailContentLabel = new StaticLabel("We sent a confirmation email to:");
            EmailContentLabel.Content.FontSize = Units.FontSizeL;
            EmailContentLabel.CenterAlign();

            StaticLabel EmailLabel = new StaticLabel(AppSession.CurrentUserDetails.email);
            EmailLabel.Content.FontSize = Units.FontSizeL;
            EmailLabel.Content.FontFamily = Fonts.GetBoldFont();
            EmailLabel.CenterAlign();

            StaticLabel EmailLinkLabel = new StaticLabel("Check your email and click on the confirmation link to continue");
            EmailLinkLabel.Content.FontSize = Units.FontSizeL;
            EmailLinkLabel.Content.Margin = new Thickness(30, 0, 30, 0);
            EmailLinkLabel.CenterAlign();

            StaticLabel ResendLabel = new StaticLabel("Resend new code");
            ResendLabel.Content.FontSize = Units.FontSizeXL;
            ResendLabel.Content.TextColor = Color.FromHex(Colors.EC_GREEN_3);
            ResendLabel.Content.FontFamily = Fonts.GetBoldFont();
            ResendLabel.Content.Margin = new Thickness(0, 50, 0, 0);
            ResendLabel.CenterAlign();

            ResendLabel.Content.GestureRecognizers.Add(
                  new TapGestureRecognizer()
                  {
                      Command = new Command(() =>
                      {
                          Device.BeginInvokeOnMainThread(async () =>
                          {
                              await App.ShowLoading();
                              ResendEmailServiceCall();
                          });
                      })
                  }
              );

            ColourButton OkButton = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "OK", null);
            OkButton.Content.VerticalOptions = LayoutOptions.CenterAndExpand;
            OkButton.Content.Margin = new Thickness(0, 30, 0, 30);

            OkButton.Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Welcome);
                           });
                       })
                   }
               );

            EmailDetailsSection = new Grid
            {
                BackgroundColor = Color.White,
            };

            StackLayout SectionContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 16
            };

            SectionContainer.Children.Add(ConfirmEmailLabel.Content);
            SectionContainer.Children.Add(EmailImage.Content);
            SectionContainer.Children.Add(EmailContentLabel.Content);
            SectionContainer.Children.Add(EmailLabel.Content);
            SectionContainer.Children.Add(EmailLinkLabel.Content);
            SectionContainer.Children.Add(ResendLabel.Content);
            SectionContainer.Children.Add(OkButton.Content);
            EmailDetailsSection.Children.Add(SectionContainer);
            return EmailDetailsSection;
        }

        private async void ResendEmailServiceCall()
        {
            var result = await App.ApiBridge.ResendEmail(AppSession.CurrentUserDetails.email);
            if (result != null)
            {
                await App.HideLoading();
                if (!result.error)
                {
                    App.ShowAlert("Alert", result.message);
                }
            }
        }
    }
}

