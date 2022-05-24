using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Fields;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Layouts.Custom;
using eCups.Layouts.Custom.Panels;
using eCups.Layouts.Custom.Tiles;
using Xamarin.Forms;
using eCups.Models;
using eCups.Renderers;
using eCups.Services;

namespace eCups.Pages.Custom
{
    public class SignUp : Page
    {
        StackLayout ContentContainer;
        StackLayout MainLayout;

        Grid TopSection;
        StaticImage TopSectionBackground;
        StaticImage TopSectionLogo;
        StackLayout TopSectionProgress;
        StaticLabel TopSectionSectionTitle;

        ProgressIndicator ProgressIndicator;

        Grid PersonalDetailsSection;

        Grid ContactDetailsSection;
        Grid UserAccountSection;
        Grid ScanQRCodeSection;
        Grid AccountConfirmationSection;
        Grid DecorSection;
        Grid BottomDecorSection;
        Grid ButtonSection;

        ColourButton ContinueButton;
        ColourButton CreateAccountButton;

        BorderInputField FirstName;
        BorderInputField Surname;
        BorderInputField DateOfBirth;
        BorderInputField EmailAddress;
        BorderInputField ContactNumber;
        BorderInputField AddressLine1;
        BorderInputField AddressLine2;
        BorderInputField AddressLine3;
        BorderInputField PostCode;
        BorderInputField Username;
        BorderInputField Password;
        BorderInputField PasswordAgain;

        User user;



        // stick these in global app settings lad
        public const int USER_DETAILS = 0;
        public const int ADD_ECUP = 1;
        public const int FINAL_SETUP = 2;


        public SignUp()
        {

            this.IsScrollable = true;
            this.HasHeader = false;
            this.Id = (int)AppSettings.PageNames.SignUp;
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
                BackgroundColor = Color.FromHex(Colors.EC_GRADIENT_START),// FromHex("eeeeee"),
                WidthRequest = Units.ScreenWidth,
                Spacing = 0
            };

            switch (AppSession.SignUpStage)
            {
                case USER_DETAILS:
                    BuildUserDetailsView();
                    break;
                case ADD_ECUP:
                    BuildAddEcupView();
                    break;
                case FINAL_SETUP:
                    BuildFinalSetupView();
                    break;
            }

            return MainLayout;
        }

        private void BuildUserDetailsView()
        {
            MainLayout.Children.Clear();
            MainLayout.Children.Add(BuildTopSection());
            MainLayout.Children.Add(BuildPersonalDetailsSection());
            MainLayout.Children.Add(BuildContactDetailsSection());
            MainLayout.Children.Add(BuildUserAccountSection());
            MainLayout.Children.Add(BuildDecorSection());
            MainLayout.Children.Add(BuildButtonSection());
        }

        private void BuildAddEcupView()
        {
            MainLayout.Children.Clear();
            MainLayout.Children.Add(BuildTopSection());
            MainLayout.Children.Add(BuildScanQRCodeSection());

            //MainLayout.Children.Add(BuildButtonSection());
            MainLayout.Children.Add(BuildBottomDecorSection());

        }

        private void BuildFinalSetupView()
        {
            MainLayout.Children.Clear();
            MainLayout.Children.Add(BuildTopSection());
            MainLayout.Children.Add(BuildAccountConfirmationSection());

            //MainLayout.Children.Add(BuildButtonSection());

            MainLayout.Children.Add(BuildBottomDecorSection());
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

            //TopSection.Margin = new Thickness(0, 0, 0, 80);

            TopSectionBackground = new StaticImage("top_decor.png", Units.ScreenWidth, null);
            TopSectionBackground.Content.Aspect = Aspect.Fill;

            TopSectionLogo = new StaticImage("ecups_logo.png", 92, null);
            TopSectionLogo.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            TopSectionLogo.Content.Margin = new Thickness(Units.ScreenHeight5Percent, 40, Units.ScreenHeight5Percent, 0);

            TopSectionProgress = new StackLayout
            {
                WidthRequest = Units.ScreenWidth,
                HorizontalOptions = LayoutOptions.CenterAndExpand,

            };

            TopSectionProgress.Children.Add(new Label
            {

            });

            ProgressIndicator = new ProgressIndicator();

            TopSectionSectionTitle = new StaticLabel("Sign Up Process");
            TopSectionSectionTitle.Content.TextColor = Color.White;
            TopSectionSectionTitle.Content.FontSize = Units.FontSizeXXL;
            TopSectionSectionTitle.Content.FontFamily = Fonts.GetBoldFont();
            TopSectionSectionTitle.LeftAlign();
            TopSectionSectionTitle.Content.VerticalOptions = LayoutOptions.StartAndExpand;
            TopSectionSectionTitle.Content.VerticalTextAlignment = TextAlignment.Start;
            TopSectionSectionTitle.Content.Margin = new Thickness(Units.ScreenHeight5Percent, 0, Units.ScreenHeight5Percent, 0);

            TopSectionContent.Children.Add(TopSectionLogo.Content);
            TopSectionContent.Children.Add(TopSectionSectionTitle.Content);
            //TopSectionContent.Children.Add(TopSectionProgress);
            TopSectionContent.Children.Add(ProgressIndicator.Content);
            TopSection.Children.Add(TopSectionBackground.Content, 0, 0);
            TopSection.Children.Add(TopSectionContent, 0, 0);


            ProgressIndicator.SetProgess(AppSession.SignUpStage + 1);

            ProgressIndicator.StepIcons[USER_DETAILS].Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               SetSection(USER_DETAILS);
                           });
                       })
                   }
               );

            ProgressIndicator.StepIcons[ADD_ECUP].Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {
                               SetSection(ADD_ECUP);
                           });
                       })
                   }
               );

            ProgressIndicator.StepIcons[FINAL_SETUP].Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {

                               SetSection(FINAL_SETUP);
                           });
                       })
                   }
               );




            return TopSection;

        }

        private Grid BuildPersonalDetailsSection()
        {
            StaticLabel PersonalDetailsLabel = new StaticLabel("Personal Details");
            PersonalDetailsLabel.Content.FontSize = Units.FontSizeXL;

            FirstName = new BorderInputField("First Name", Keyboard.Text, true);
            Surname = new BorderInputField("Surname", Keyboard.Text, true);
            DateOfBirth = new BorderInputField("Date of Birth", Keyboard.Text, true);

            PersonalDetailsSection = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(Units.ScreenWidth10Percent)
            };

            StackLayout SectionContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Spacing = 16
            };

            SectionContainer.Children.Add(PersonalDetailsLabel.Content);
            SectionContainer.Children.Add(FirstName.Content);
            SectionContainer.Children.Add(Surname.Content);
            SectionContainer.Children.Add(DateOfBirth.Content);

            PersonalDetailsSection.Children.Add(SectionContainer);

            FirstName.SetLightBackgroundTheme();
            Surname.SetLightBackgroundTheme();
            DateOfBirth.SetLightBackgroundTheme();

            return PersonalDetailsSection;
        }

        private Grid BuildContactDetailsSection()
        {
            StaticLabel ContactlDetailsLabel = new StaticLabel("Contact Details");
            ContactlDetailsLabel.Content.FontSize = Units.FontSizeXL;

            EmailAddress = new BorderInputField("Email Address", Keyboard.Email, true);
            ContactNumber = new BorderInputField("Contact No.", Keyboard.Telephone, true);
            AddressLine1 = new BorderInputField("Address Line 1", Keyboard.Text, false);
            AddressLine2 = new BorderInputField("Address Line 2", Keyboard.Text, false);
            AddressLine3 = new BorderInputField("Address Line 3", Keyboard.Text, false);
            PostCode = new BorderInputField("Postcode", Keyboard.Text, false);

            ContactDetailsSection = new Grid
            {
                BackgroundColor = Color.FromHex(Colors.EC_SUPER_LIGHT_GREY),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(Units.ScreenWidth10Percent)
            };

            StackLayout SectionContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Spacing = 16
            };

            SectionContainer.Children.Add(ContactlDetailsLabel.Content);
            SectionContainer.Children.Add(EmailAddress.Content);
            SectionContainer.Children.Add(ContactNumber.Content);
            SectionContainer.Children.Add(AddressLine1.Content);
            SectionContainer.Children.Add(AddressLine2.Content);
            SectionContainer.Children.Add(AddressLine3.Content);
            SectionContainer.Children.Add(PostCode.Content);


            ContactDetailsSection.Children.Add(SectionContainer);


            EmailAddress.SetLightBackgroundTheme();
            ContactNumber.SetLightBackgroundTheme();
            AddressLine1.SetLightBackgroundTheme();
            AddressLine2.SetLightBackgroundTheme();
            AddressLine3.SetLightBackgroundTheme();
            PostCode.SetLightBackgroundTheme();

            return ContactDetailsSection;
        }

        private Grid BuildUserAccountSection()
        {
            StaticLabel UserAccountLabel = new StaticLabel("User Account");
            UserAccountLabel.Content.FontSize = Units.FontSizeXL;

            Username = new BorderInputField("Username", Keyboard.Text, true);
            Password = new BorderInputField("Password", Keyboard.Text, true);
            PasswordAgain = new BorderInputField("Re-type Password", Keyboard.Text, true);


            UserAccountSection = new Grid
            {
                BackgroundColor = Color.FromHex(Colors.EC_GREEN_2),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(Units.ScreenWidth10Percent)
            };

            StackLayout SectionContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Spacing = 16
            };

            SectionContainer.Children.Add(UserAccountLabel.Content);
            SectionContainer.Children.Add(Username.Content);
            SectionContainer.Children.Add(Password.Content);
            SectionContainer.Children.Add(PasswordAgain.Content);

            UserAccountSection.Children.Add(SectionContainer);

            Username.SetDarkBackgroundTheme();
            Password.SetDarkBackgroundTheme();
            PasswordAgain.SetDarkBackgroundTheme();

            return UserAccountSection;
        }


        private Grid BuildScanQRCodeSection()
        {
            ScanQRCodeSection = new ScanTile().Content;

            return ScanQRCodeSection;
        }


        private Grid BuildAccountConfirmationSection()
        {
            AccountConfirmationSection = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(Units.ScreenWidth10Percent)
            };

            StackLayout SectionContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Spacing = 16
            };

            StaticLabel EcupNearlyReadyLabel = new StaticLabel("Your Ecup is Nearly Ready!");
            EcupNearlyReadyLabel.Content.FontSize = Units.FontSizeXL;

            StaticLabel EcupNearlReadyDetail = new StaticLabel("Before your final set up we need you to confirm your details and the Ecup registered to this account.");
            EcupNearlReadyDetail.LeftAlign();
            EcupNearlReadyDetail.Content.TextColor = Color.FromHex(Colors.EC_GREEN_2);
            EcupNearlReadyDetail.Content.FontSize = Units.FontSizeL;

            StaticLabel YourEcupLabel = new StaticLabel("Your Ecup:");
            YourEcupLabel.Content.FontSize = Units.FontSizeXL;

            StaticLabel YourDetailsLabel = new StaticLabel("Your Details:");
            YourDetailsLabel.Content.FontSize = Units.FontSizeXL;


            StaticImage CupImage = new StaticImage("cup.png", 64, null);
            CupImage.Content.HorizontalOptions = LayoutOptions.StartAndExpand;

            StaticLabel EcupSerialNumberTitle = new StaticLabel("Ecup Serial No:");
            EcupSerialNumberTitle.LeftAlign();
            EcupSerialNumberTitle.Content.TextColor = Color.Black;
            EcupSerialNumberTitle.Content.FontSize = Units.FontSizeL;

            StaticLabel EcupSerialNumber = new StaticLabel(AppSession.CurrentUser.ActiveEcup.SerialNumber);
            EcupSerialNumber.LeftAlign();
            EcupSerialNumber.Content.TextColor = Color.FromHex(Colors.EC_GREEN_2);
            EcupSerialNumber.Content.FontSize = Units.FontSizeM;

            StaticLabel RegisteredDateTitle = new StaticLabel("Registered Date:");
            RegisteredDateTitle.LeftAlign();
            RegisteredDateTitle.Content.TextColor = Color.Black;
            RegisteredDateTitle.Content.FontSize = Units.FontSizeL;

            StaticLabel RegisteredDate = new StaticLabel(AppSession.CurrentUser.ActiveEcup.RegisteredDate.ToShortDateString());
            RegisteredDate.LeftAlign();
            RegisteredDate.Content.TextColor = Color.FromHex(Colors.EC_GREEN_2);
            RegisteredDate.Content.FontSize = Units.FontSizeM;

            Grid ECupDetailsContainer = new Grid { HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };

            ECupDetailsContainer.Children.Add(CupImage.Content, 0, 0);
            ECupDetailsContainer.Children.Add(EcupSerialNumberTitle.Content, 1, 0);
            ECupDetailsContainer.Children.Add(EcupSerialNumber.Content, 1, 1);
            ECupDetailsContainer.Children.Add(RegisteredDateTitle.Content, 1, 2);
            ECupDetailsContainer.Children.Add(RegisteredDate.Content, 1, 3);

            Grid.SetRowSpan(CupImage.Content, 5);


            Grid YourDetailsContainer = new Grid { };

            YourDetailsContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            YourDetailsContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            YourDetailsContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            YourDetailsContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            YourDetailsContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            ComplexLabel NameLabel = new ComplexLabel("Name", "Mat Howlett", true, false);
            ComplexLabel EmailLabel = new ComplexLabel("Email", "mathowlett@gmail.com", true, false);
            ComplexLabel UserLabel = new ComplexLabel("User", "mathowlett", true, false);
            ComplexLabel Password = new ComplexLabel("Password", "**********", true, true);



            YourDetailsContainer.Children.Add(NameLabel.Content, 0, 0);
            YourDetailsContainer.Children.Add(EmailLabel.Content, 0, 1);
            YourDetailsContainer.Children.Add(new Grid { WidthRequest = Units.ScreenWidth, HeightRequest = 1, VerticalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.FromHex(Colors.EC_SUPER_LIGHT_GREY) }, 0, 2);
            YourDetailsContainer.Children.Add(UserLabel.Content, 0, 3);
            YourDetailsContainer.Children.Add(Password.Content, 0, 4);

            StaticLabel TermsAndConditionsLabel = new StaticLabel("");

            var formattedString = new FormattedString();
            formattedString.Spans.Add(new Span { Text = "I agree to the Ecup ", FontSize = Units.FontSizeM, TextColor = Color.Gray });
            formattedString.Spans.Add(new Span { Text = "Terms & Conditions", FontSize = Units.FontSizeM, TextColor = Color.FromHex(Colors.EC_GREEN_2), TextDecorations = TextDecorations.Underline });
            formattedString.Spans.Add(new Span { Text = ". I agree to everything you ask me, so yeah, it's all good.", FontSize = Units.FontSizeM, TextColor = Color.Gray });
            TermsAndConditionsLabel.Content.FormattedText = formattedString;
            TermsAndConditionsLabel.CenterAlign();

            SectionContainer.Children.Add(EcupNearlyReadyLabel.Content);
            SectionContainer.Children.Add(EcupNearlReadyDetail.Content);


            SectionContainer.Children.Add(YourEcupLabel.Content);
            SectionContainer.Children.Add(ECupDetailsContainer);


            SectionContainer.Children.Add(YourDetailsLabel.Content);
            SectionContainer.Children.Add(YourDetailsContainer);
            SectionContainer.Children.Add(TermsAndConditionsLabel.Content);

            CreateAccountButton = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "Create Your Account", null);

            CreateAccountButton.Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {

                               ProcessCreateAccount();
                           });
                       })
                   }
               );

            CreateAccountButton.Content.VerticalOptions = LayoutOptions.CenterAndExpand;
            SectionContainer.Children.Add(CreateAccountButton.Content);

            AccountConfirmationSection.Children.Add(SectionContainer);

            return AccountConfirmationSection;
        }

        private Grid BuildBottomDecorSection()
        {
            BottomDecorSection = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.EndAndExpand,
                ColumnSpacing = 0,
                RowSpacing = 0,
                HeightRequest = 92
            };

            Image decor = new Image { Source = "bottom_decor2.png", Aspect = Aspect.Fill, HeightRequest = 92, VerticalOptions = LayoutOptions.EndAndExpand };

            BottomDecorSection.Children.Add(decor, 0, 0);

            return BottomDecorSection;
        }

        private Grid BuildDecorSection()
        {
            DecorSection = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.StartAndExpand,
                ColumnSpacing = 0,
                RowSpacing = 0,
                HeightRequest = 180
            };

            Image decor = new Image { Source = "mid_decor.png", Aspect = Aspect.Fill, HeightRequest = 180, VerticalOptions = LayoutOptions.CenterAndExpand };


            DecorSection.Children.Add(new Grid { BackgroundColor = Color.FromHex(Colors.EC_GREEN_2) }, 0, 0);
            DecorSection.Children.Add(new Grid { BackgroundColor = Color.FromHex(Colors.EC_GREEN_2) }, 0, 1);
            DecorSection.Children.Add(new Grid { BackgroundColor = Color.White }, 0, 2);


            DecorSection.Children.Add(decor, 0, 0);

            Grid.SetRowSpan(decor, 3);

            return DecorSection;
        }


        private Grid BuildButtonSection()
        {
            ButtonSection = new Grid
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HeightRequest = 120
            };

            ContinueButton = new ColourButton(Color.FromHex(Colors.EC_GREEN_3), Color.White, "Continue", null);

            ContinueButton.Content.GestureRecognizers.Add(
                   new TapGestureRecognizer()
                   {
                       Command = new Command(() =>
                       {
                           Device.BeginInvokeOnMainThread(async () =>
                           {

                               ProcessContinue();
                           });
                       })
                   }
               );

            ContinueButton.Content.VerticalOptions = LayoutOptions.CenterAndExpand;

            Grid TopSection = new Grid
            {

            };
            ButtonSection.Children.Add(ContinueButton.Content, 0, 0);

            return ButtonSection;
        }

        private async void ProcessCreateAccount()
        {
            await Task.Delay(10);

            AppSession.SignUpStage = 0;
            await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.AccountCreated);

        }

        private async void ProcessContinue()
        {
            await Task.Delay(10);
            switch (AppSession.SignUpStage)
            {
                case USER_DETAILS:
                    //if (AppSession.SignUpStage < 2)
                    //{
                    //NextSection();
                    //}
                    //else
                    //{
                    //    AppSession.SignUpStage = 0;
                    //    await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.FirstLoad);
                    //}

                    // await App.ShowLoading();
                    RegisterServiceCall();
                    // await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Welcome);
                    break;
                case ADD_ECUP:
                    if (AppSession.CupScanned)
                    {
                        NextSection();
                    }
                    else
                    {
                        App.ShowAlert("Please scan your cup before proceeding");
                    }

                    break;
                case FINAL_SETUP:
                    App.ShowAlert("Please scan your cup before proceeding");
                    break;
            }
        }

        private void NextSection()
        {
            AppSession.SignUpStage++;

            PageContent.Children.Remove(ContentContainer);
            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
        }



        private void SetSection(int section)
        {
            AppSession.SignUpStage = section;

            PageContent.Children.Remove(ContentContainer);
            ContentContainer = BuildContent();
            PageContent.Children.Add(ContentContainer);
        }



        public override async Task Update()
        {
            if (this.NeedsRefreshing)
            {
                await DebugUpdate(AppSettings.TransitionVeryFast);

                //this.HasHeader = false;
                //this.HasFooter = true;
                await base.Update();

                PageContent.Children.Remove(ContentContainer);
                ContentContainer = BuildContent();
                PageContent.Children.Add(ContentContainer);
                //App.ShowMenuButton();
                App.HideMenuButton();
            }
        }

        private async void RegisterServiceCall()
        {
            user = new User
            {
                FirstName = FirstName.GetText(),
                MiddleName = Surname.GetText(),
                DateOfBirth = DateTime.Now.ToString("yyyy-MM-dd"),
                EmailAddress = EmailAddress.GetText(),
                MobileNumber = ContactNumber.GetText(),
                AddressLine1 = AddressLine1.GetText(),
                AddressLine2 = AddressLine2.GetText(),
                AddressLine3 = AddressLine3.GetText(),
                AreaCode = PostCode.GetText(),
                Username = Username.GetText(),
                Password = Password.GetText(),
                DeviceType = "mobile"
            };
            var result = await App.ApiBridge.Register(user);
            if (result != null)
            {
                await App.HideLoading();
                if (result.error)
                {
                    App.ShowAlert("Alert", result.message);
                }
                else
                {
                    AppSession.CurrentUser = result;
                    AppSession.CurrentUserDetails = result.details;
                    await App.PerformActionAsync((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.WelcomeLogin);
                    //NextSection();
                }
            }

        }
    }
}