using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Composites;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Helpers.Custom;
using eCups.Layouts;
using eCups.Layouts.Custom;
using eCups.Layouts.Custom.Tiles;
using eCups.Pages;
using eCups.Pages.Custom;
using Xamarin.Forms;

namespace eCups
{

    // The AppContainer is the controlling view that handles the loading,
    // unloading, transitioning and overall presentation of all other pages.

    public class AppContainter : ContentPage
    {
        public new int Width { get; set; }
        public new int Height { get; set; }

        private AppSettings AppSettings;

        // the top level container for all other content, including overlays and slide on menus
        private Layer ParentContainer;

        // holds header, main content layers and footer
        private Layer MainLayoutContainer;

        private StackLayout MainLayoutStack;

        // holds all over layers below
        private Layer MainLayout;

        // main background layer
        private Layer BackgroundLayer;

        // effects, parallax, etc
        private Layer BackgroundOverlayLayer;

        // main page layer
        private Layer ContentLayer;

        // panels layer
        private Layer PanelLayer;

        // menu layer
        private Layer MenuLayer;

        // anything presented on top of main content
        private Layer ForegroundLayer;

        // effects, parallax, etc
        private Layer ForegroundOverlayLayer;

        // full screen modal layer
        private Layer ModalLayer;

        // loading spinner
        private ActivityIndicator Spinner;

        private Layouts.Header Header;
        private Layouts.Footer Footer;

        //private Layouts.Menu Menu;

        private int ContentHeight;

        private MainHeader MainHeader;
        private SubHeader SubHeader;
        private NavHeader NavHeader;

        public AppContainter(int width, int height)
        {
            // full screen height
            Width = width;
            Height = height;
            ContentHeight = height;

            AppSettings = new AppSettings();
            AppSettings.FullScreenHeight = height;

            Content = BuildAppLayout(Width, Height).Layout;

            Populate();
        }

        private Layer BuildAppLayout(int width, int height)
        {
            // create all layers
            ParentContainer = new Layer(width, height);

            // stack to hold header, main layout and footer (header and footer are optional)
            MainLayoutStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                WidthRequest = width,
                HeightRequest = height,
                Spacing = 0,
                BackgroundColor = Color.FromHex(Colors.EC_GRADIENT_START)
            };

            // stack to hold header, main layout and footer (header and footer are optional)
            BackgroundLayer = new Layer(width, height);
            BackgroundLayer.Deactivate();

            BackgroundOverlayLayer = new Layer(width, height);
            BackgroundOverlayLayer.Deactivate();

            ContentLayer = new Layer(width, height);
            ContentLayer.Deactivate();

            MenuLayer = new Layer(width, height);

            // add the menu to the content layer
            MenuLayer.Layout.Children.Add(new Pages.Custom.Menu().GetContent());
            MenuLayer.Layout.TranslateTo(0, -Units.ScreenHeight, 0, Easing.Linear);
            MenuLayer.Deactivate();

            PanelLayer = new Layer(width, height);
            PanelLayer.Layout.BackgroundColor = Color.FromHex(Colors.CC_MAIN_GREY);
            PanelLayer.Layout.Opacity = 1;
            PanelLayer.Layout.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await Update();
                                await TogglePanel();
                            });
                        })
                    }
                );

            ForegroundLayer = new Layer(width, height);
            ForegroundLayer.Layout.BackgroundColor = Color.Transparent;
            ForegroundLayer.Layout.Opacity = 1;
            ForegroundLayer.Layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
            ForegroundLayer.Layout.VerticalOptions = LayoutOptions.CenterAndExpand;
            ForegroundLayer.Layout.Margin = new Thickness(0, Units.ScreenHeight25Percent, 0, Units.ThirdScreenHeight);

            Spinner = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = Units.ScreenWidth25Percent,
                WidthRequest = Units.ScreenWidth25Percent,
                Color = Color.White,
                IsRunning = false,
            };

            //ForegroundLayer.Layout.Opacity = 0;
            //ForegroundLayer.Layout.Children.Add(Spinner);
            //ForegroundLayer.Deactivate();

            ForegroundOverlayLayer = new Layer(width, height);
            ForegroundOverlayLayer.Layout.BackgroundColor = Color.Black;
            ForegroundOverlayLayer.Layout.Opacity = 0;
            ForegroundOverlayLayer.Layout.Children.Add(Spinner);
            ForegroundOverlayLayer.Deactivate();

            ModalLayer = new Layer(width, height);
            ModalLayer.Layout.HorizontalOptions = LayoutOptions.Center;
            ModalLayer.Layout.VerticalOptions = LayoutOptions.Center;
            ModalLayer.Layout.BackgroundColor = Color.Black;
            ModalLayer.Layout.Opacity = AppSettings.ModalOpacity;
            ModalLayer.Layout.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await Update();
                                await ToggleModal();
                            });
                        })
                    }
                );
            ModalLayer.Deactivate();

            if (AppSettings.HasStatusBar)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    MainLayoutStack.Padding = new Thickness(0, AppSettings.StatusBarHeight, 0, 0);
                }
            }

            // create header, if required
            if (AppSettings.HasHeader)
            {
                Header = new Layouts.Header();
                //Header.SetHeight(AppSettings.HeaderHeight); // test
                MainHeader = new MainHeader();
                Header.Content = MainHeader.Content;

                SubHeader = new SubHeader();
                NavHeader = new NavHeader();

                HideHeader();
                HideSubHeader();
                HideNavHeader();

                Header.Content.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            //ToggleMenu();// LayoutTest();
                        })
                    }
                );
            }

            // create footer, if required
            if (AppSettings.HasFooter)
            {
                Footer = new Layouts.Footer();
                Footer.Content.HeightRequest = AppSettings.FooterHeight; // test
            }

            // add the header to the main layout stack
            if (AppSettings.HasHeader)
            {
                MainLayoutStack.Children.Add(Header.Content);
            }

            if (AppSettings.HasSubHeader)
            {
                MainLayoutStack.Children.Add(SubHeader.Content);
            }

            if (AppSettings.HasNavHeader)
            {
                MainLayoutStack.Children.Add(NavHeader.Content);
            }

            // create main content layer, always
            MainLayout = new Layer(width, ContentHeight);
            MainLayout.Layout.Children.Add(ContentLayer.Layout);
            MainLayoutStack.Children.Add(MainLayout.Layout);

            // add the footer to the main layout stack
            if (AppSettings.HasFooter)
            {
                MainLayoutStack.Children.Add(Footer.Content);
            }


            // replace standard background with a background image
            MainLayoutStack.BackgroundColor = Color.FromHex(Colors.EC_GRADIENT_START);
            //BackgroundLayer.AddBackgroundImage("sep2.jpg");
            //BackgroundLayer.SetBackGroundImageAspect(Aspect.AspectFill);
            BackgroundLayer.Activate();

            ParentContainer.Layout.Children.Add(BackgroundLayer.Layout, 0, 0);
            ParentContainer.Layout.Children.Add(BackgroundOverlayLayer.Layout, 0, 0);
            ParentContainer.Layout.Children.Add(MainLayoutStack, 0, 0);
            ParentContainer.Layout.Children.Add(PanelLayer.Layout, 0, 0);
            ParentContainer.Layout.Children.Add(MenuLayer.Layout, 0, 0);

            ParentContainer.Layout.Children.Add(ForegroundLayer.Layout, 0, 0);
            ParentContainer.Layout.Children.Add(ForegroundOverlayLayer.Layout, 0, 0);
            ParentContainer.Layout.Children.Add(ModalLayer.Layout, 0, 0);

            PanelLayer.Deactivate();
            ForegroundLayer.Deactivate();
            ForegroundOverlayLayer.Deactivate();
            ModalLayer.Deactivate();

            // test
            ParentContainer.Layout.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            //ToggleMenu();
                        })
                    }
                );

            HideFooter();
            HideHeader();
            HideSubHeader();
            HideNavHeader();

            return ParentContainer;
        }

        private void Populate()
        {
            if (App.IsBusy) return;
            App.IsBusy = true;

            Device.BeginInvokeOnMainThread(async () =>
            {

                ////////// POPULATE CONTENT LAYER //////////
                //Helpers.Pages.AddPage(new Pages.Custom.FirstLoad());
                //Helpers.Pages.AddPage(new Pages.Custom.AboutUs());
                //Helpers.Pages.AddPage(new Pages.Custom.Menu());

                Helpers.Pages.AddPage(new Pages.Custom.FirstLoad());
                Helpers.Pages.AddPage(new Pages.Custom.CafePage());
                Helpers.Pages.AddPage(new Pages.Custom.ChangeLocation());
                Helpers.Pages.AddPage(new Pages.Custom.DealsLanding());
                Helpers.Pages.AddPage(new Pages.Custom.Home());
                Helpers.Pages.AddPage(new Pages.Custom.Landing());
                Helpers.Pages.AddPage(new Pages.Custom.Map());
                Helpers.Pages.AddPage(new Pages.Custom.Menu());
                Helpers.Pages.AddPage(new Pages.Custom.QRScanner());
                Helpers.Pages.AddPage(new Pages.Custom.SignUp());
                Helpers.Pages.AddPage(new Pages.Custom.StoresLanding());
                Helpers.Pages.AddPage(new Pages.Custom.Template());
                Helpers.Pages.AddPage(new Pages.Custom.Welcome());
                Helpers.Pages.AddPage(new Pages.Custom.WelcomeLogin());
                Helpers.Pages.AddPage(new Pages.Custom.YourAccount());
                Helpers.Pages.AddPage(new Pages.Custom.YourRewards());
                Helpers.Pages.AddPage(new Pages.Custom.SettingsPage());
                Helpers.Pages.AddPage(new Pages.Custom.AccountCreated());
                Helpers.Pages.AddPage(new Pages.Custom.NewLevel());
                Helpers.Pages.AddPage(new Pages.Custom.CodeScanned());

                // we can do this elsewhere, but for now, add it here
                
                ContentLayer.Layout.Children.Add(Helpers.Pages.GetPageById(AppSettings.FirstPage).GetContent()); //new TestPage1().Content);
                Helpers.Pages.GetCurrent().Update();                

                ContentLayer.Activate();

                if (Helpers.Pages.GetCurrent().HasFooter)
                {
                    ShowFooter();
                }
                else
                {
                    HideFooter();
                }

                if (Helpers.Pages.GetCurrent().HasHeader)
                {
                    await ShowHeader();
                }
                else
                {
                    await HideHeader();
                }

                if (Helpers.Pages.GetCurrent().HasSubHeader)
                {
                    await ShowSubHeader();
                }
                else
                {
                    await HideSubHeader();
                }

                if (Helpers.Pages.GetCurrent().HasNavHeader)
                {
                    await ShowNavHeader();
                }
                else
                {
                    await HideNavHeader();
                }

                ////////// END POPULATE CONTENT LAYER //////////////////////////
                ///
                // activate the content layer
                ContentLayer.Activate();
            });

            App.IsBusy = false;
        }

        // header
        public async Task<bool> ToggleHeader()
        {
            if (Header.Content.IsVisible)
            {
                await HideHeader();
            }
            else
            {
                await ShowHeader();
            }
            return true;
        }

        public async Task<bool> HideHeader()
        {
            await Header.Hide();
            UpdateContentSizeAndPosition();
            return true;
        }

        public async Task<bool> ShowHeader()
        {
            await Header.Show();
            UpdateContentSizeAndPosition();
            return true;
        }

        // sub header
        public async Task<bool> ToggleSubHeader()
        {
            if (SubHeader.Content.IsVisible)
            {
                await HideSubHeader();
            }
            else
            {
                await ShowSubHeader();
            }
            return true;
        }

        public async Task<bool> HideSubHeader()
        {
            await SubHeader.Hide();
            UpdateContentSizeAndPosition();
            return true;
        }

        public async Task<bool> ShowSubHeader()
        {
            await SubHeader.Show();
            UpdateContentSizeAndPosition();
            return true;
        }

        // nav header
        public async Task<bool> ToggleNavHeader()
        {
            if (NavHeader.Content.IsVisible)
            {
                await HideNavHeader();
            }
            else
            {
                await ShowNavHeader();
            }
            return true;
        }

        public async Task<bool> HideNavHeader()
        {
            await NavHeader.Hide();
            UpdateContentSizeAndPosition();
            return true;
        }

        public async Task<bool> ShowNavHeader()
        {
            await NavHeader.Show();
            UpdateContentSizeAndPosition();
            return true;
        }

        // modal
        public async Task<bool> ToggleModal()
        {
            if (ModalLayer.IsActive())
            {
                await HideModal();
            }
            else
            {
                await ShowModal();
            }
            return true;
        }

        public async Task<bool> ShowModal()
        {
            ModalLayer.Layout.Children.Clear();

            // add content to modal here
            await Task.Delay((int)AppSettings.TransitionVeryFast);

            await ModalLayer.Layout.FadeTo(AppSettings.ModalOpacity, 250, Easing.Linear);
            ModalLayer.Activate();
            ParentContainer.Layout.Children.Add(ModalLayer.Layout, 0, 0);
            return true;

        }

        public async Task<bool> HideModal()
        {
            await ModalLayer.Layout.FadeTo(0, 250, Easing.Linear);
            ModalLayer.Deactivate();
            ModalLayer.Layout.Children.Clear();
            ParentContainer.Layout.Children.Remove(ModalLayer.Layout);
            return true;
        }

        // foreground
        public async Task<bool> ToggleForeground()
        {
            if (ForegroundLayer.IsActive())
            {
                await HideForeground();
            }
            else
            {
                await ShowForeground();
            }
            return true;
        }

        public async Task<bool> ShowForeground()
        {
            ForegroundLayer.Layout.Children.Clear();

            //await Task.Delay((int)AppSettings.TransitionVeryFast);


            // add content to modal here

            /*
            ActionResultPanel modalPanel = new ActionResultPanel(true);
            modalPanel.SetBackground(Color.Green, AppSettings.ModalOpacity);
            modalPanel.Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            modalPanel.AddContent(new ValueAdjuster("Height (m)", 1.17f, 100, 32).GetContent());
            modalPanel.AddContent(new AlbumTile(true, 280, 280).GetContent());
            ForegroundLayer.Layout.Children.Add(modalPanel.Content);
            */

            ForegroundLayer.Layout.Children.Add(new ValueAdjuster("Height (m)", 100, 32).GetContent(), 0, 0);
            ForegroundLayer.Layout.Children.Add(new AlbumTile(true, 280, 280).GetContent(), 1, 0);

            ForegroundLayer.Activate();
            ParentContainer.Layout.Children.Add(ForegroundLayer.Layout, 0, 0);

            await ForegroundLayer.Layout.FadeTo(1, 250, Easing.Linear);
            //await ForegroundLayer.Layout.TranslateTo(0, 250, 3000, Easing.Linear);

            return true;

        }

        public async Task<bool> HideForeground()
        {
            await ForegroundLayer.Layout.FadeTo(0, 250, Easing.Linear);
            ForegroundLayer.Deactivate();
            ForegroundLayer.Layout.Children.Clear();
            ParentContainer.Layout.Children.Remove(ForegroundLayer.Layout);
            return true;
        }

        // panel
        public async Task<bool> TogglePanel()
        {
            if (PanelLayer.IsActive())
            {
                await HidePanel();
            }
            else
            {
                await ShowPanel();
            }
            return true;
        }

        public async Task<bool> ShowPanel()
        {

            PanelLayer.Layout.Children.Clear();

            // add content to modal here
            await Task.Delay((int)AppSettings.TransitionVeryFast);

            //ActionResultPanel modalPanel = new ActionResultPanel(false);
            //modalPanel.SetBackground(Color.Green, AppSettings.ModalOpacity);
            //PanelLayer.Layout.Children.Add(modalPanel.Content);

            int MarginLeft = Units.ScreenWidth - (Dimensions.RIGHT_MENU_WIDTH + Units.TapSizeXXS);
            int MarginRight = Units.TapSizeXXS;
            int MarginTop = Dimensions.HEADER_HEIGHT + Dimensions.SUBHEADER_HEIGHT + Dimensions.NAVHEADER_HEIGHT;
            PanelLayer.Layout.Margin = new Thickness(MarginLeft, MarginTop, MarginRight, Units.TapSizeXXS);

            PanelLayer.Activate();
            await PanelLayer.Layout.TranslateTo(0, 0, (uint)AppSettings.TransitionVeryFast, Easing.Linear);
            //await PanelLayer.Layout.FadeTo(1, 250, Easing.Linear);

            //ParentContainer.Layout.Children.Add(PanelLayer.Layout, 0, 0);
            return true;

        }

        public async Task<bool> HidePanel()
        {
            await PanelLayer.Layout.TranslateTo((int)(Dimensions.RIGHT_MENU_WIDTH + Units.TapSizeXXS), 0, (uint)AppSettings.TransitionVeryFast, Easing.Linear);
            //await PanelLayer.Layout.FadeTo(0, 250, Easing.Linear);
            PanelLayer.Deactivate();
            PanelLayer.Layout.Children.Clear();
            return true;
        }

        /*
        public async Task<bool> HideModal()
        {
            await ModalLayer.Layout.FadeTo(0, (uint)AppSettings.TransitionVeryFast, Easing.Linear);
            ModalLayer.Deactivate();
            return true;
        }

        public async Task<bool> ShowModal()
        {
            ModalLayer.Activate();
            await ModalLayer.Layout.FadeTo(1, (uint)AppSettings.TransitionVeryFast, Easing.Linear);            
            return true;
        }*/

        public async Task<bool> ToggleFooter()
        {
            if (AppSettings.HasFooter)
            {
                if (Footer.Content.IsEnabled)
                {
                    await HideFooter();
                }
                else
                {
                    await ShowFooter();
                }
            }
            return true;
        }

        public async Task<bool> HideFooter()
        {
            if (AppSettings.HasFooter)
            {
                await Footer.Content.TranslateTo(0, Units.FooterHeight, 150, Easing.Linear);
                Footer.Content.IsEnabled = false;
                UpdateContentSizeAndPosition();
            }
            return true;
        }

        public async Task<bool> ShowFooter()
        {
            if (AppSettings.HasFooter)
            {
                await Footer.Content.TranslateTo(0, 0, 150, Easing.Linear);
                Footer.Content.IsEnabled = true;
                UpdateContentSizeAndPosition();
            }
            return true;
        }

        public async Task<bool> ToggleMenu()
        {
            // menu is onscreen, so we're turning it off
            if (MenuLayer.Layout.IsVisible)
            {
                await HideMenu();
            }
            else
            {

                await ShowMenu();
            }
            return true;
        }

        public async Task<bool> ShowMenu()
        {
            await Task.Delay(25);
            Device.BeginInvokeOnMainThread(async () =>
            {
                MenuLayer.Activate();
                switch (AppSettings.MenuPosition)
                {
                    case (int)AppSettings.MenuPositions.Top:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                                MenuLayer.Layout.TranslateTo(0, (0 - (Units.ScreenHeight - AppSettings.MenuCoverageVertical)), 250, Easing.CubicIn)
                            );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(0, Units.HalfScreenHeight, 350, Easing.CubicIn),
                                MenuLayer.Layout.TranslateTo(0, (0 - (Units.ScreenHeight - AppSettings.MenuCoverageVertical)), 250, Easing.CubicIn)
                            );
                        }
                        break;
                    case (int)AppSettings.MenuPositions.Bottom:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                                MenuLayer.Layout.TranslateTo(0, (Units.ScreenHeight - AppSettings.MenuCoverageVertical), 250, Easing.CubicIn)
                            );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(0, -Units.HalfScreenHeight, 350, Easing.CubicIn),
                                MenuLayer.Layout.TranslateTo(0, (Units.ScreenHeight - AppSettings.MenuCoverageVertical), 250, Easing.CubicIn)
                            );
                        }
                        break;
                    case (int)AppSettings.MenuPositions.Left:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                                MenuLayer.Layout.TranslateTo(0 - (Units.ScreenWidth - AppSettings.MenuCoverageHorizontal), 0, 250, Easing.CubicIn)
                            );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(Units.ScreenWidth, 0, 350, Easing.CubicIn),
                                MenuLayer.Layout.TranslateTo(0 - (Units.ScreenWidth - AppSettings.MenuCoverageHorizontal), 0, 250, Easing.CubicIn)
                            );
                        }
                        break;
                    case (int)AppSettings.MenuPositions.Right:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                               MenuLayer.Layout.TranslateTo(Units.ScreenWidth - AppSettings.MenuCoverageHorizontal, 0, 250, Easing.CubicIn)
                           );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(-AppSettings.MenuCoverageHorizontal, 0, 350, Easing.CubicIn),
                                MenuLayer.Layout.TranslateTo(Units.ScreenWidth - AppSettings.MenuCoverageHorizontal, 0, 250, Easing.CubicIn)
                            );
                        }
                        break;
                }
            });
            return true;
        }

        public async Task<bool> HideMenu()
        {
            await Task.Delay(25);
            Device.BeginInvokeOnMainThread(async () =>
            {
                switch (AppSettings.MenuPosition)
                {
                    case (int)AppSettings.MenuPositions.Top:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                                MenuLayer.Layout.TranslateTo(0, -Units.ScreenHeight, 350, Easing.CubicOut)
                            );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(0, 0, 250, Easing.CubicOut),
                                MenuLayer.Layout.TranslateTo(0, -Units.ScreenHeight, 350, Easing.CubicOut)
                            );
                        }
                        break;
                    case (int)AppSettings.MenuPositions.Bottom:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                                MenuLayer.Layout.TranslateTo(0, Units.ScreenHeight, 350, Easing.CubicOut)
                            );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(0, 0, 250, Easing.CubicOut),
                                MenuLayer.Layout.TranslateTo(0, Units.ScreenHeight, 350, Easing.CubicOut)
                            );
                        }
                        break;
                    case (int)AppSettings.MenuPositions.Left:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                                MenuLayer.Layout.TranslateTo(-Units.ScreenWidth, 0, 350, Easing.CubicOut)
                            );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(0, 0, 250, Easing.CubicOut),
                                MenuLayer.Layout.TranslateTo(-Units.ScreenWidth, 0, 350, Easing.CubicOut)
                            );
                        }
                        break;
                    case (int)AppSettings.MenuPositions.Right:
                        if (AppSettings.MenuShownOverContent)
                        {
                            await Task.WhenAll(
                                MenuLayer.Layout.TranslateTo(Units.ScreenWidth, 0, 350, Easing.CubicOut)
                            );
                        }
                        else
                        {
                            await Task.WhenAll(
                                MainLayoutStack.TranslateTo(0, 0, 250, Easing.CubicOut),
                                MenuLayer.Layout.TranslateTo(Units.ScreenWidth, 0, 350, Easing.CubicOut)
                            );
                        }
                        break;

                }
                MenuLayer.Deactivate();
            });
            return true;
        }

        private void UpdateContentSizeAndPosition()
        {
            // not currently used
        }

        public async Task<bool> HardwareBack()
        {
            await GoToLastPage();
            return true;
        }

        public void ShowMenuButton()
        {
            MainHeader.ShowMenuButton();
        }

        public void HideMenuButton()
        {
            MainHeader.HideMenuButton();
        }

        private void Update()
        {
            // keep header and footer on top
            if (AppSettings.HasHeader)
            {
                MainLayoutStack.RaiseChild(Header.Content);
            }
            MainLayoutStack.RaiseChild(MainLayout.Layout);
            if (AppSettings.HasFooter)
            {
                MainLayoutStack.RaiseChild(Footer.Content);
            }
        }

        public async Task<bool> ShowLoading()
        {
            App.IsBusy = true;
            ForegroundOverlayLayer.Layout.BackgroundColor = Color.Black;
            ForegroundOverlayLayer.Activate();
            ParentContainer.Layout.RaiseChild(ForegroundOverlayLayer.Layout);
            await ForegroundOverlayLayer.Layout.FadeTo(0.75, 250, Easing.Linear);
            ForegroundOverlayLayer.Layout.IsVisible = true;
            ForegroundOverlayLayer.Layout.IsEnabled = true;
            Spinner.IsRunning = true;
            return true;
        }

        public async Task<bool> HideLoading()
        {
            Spinner.IsRunning = false;
            await ForegroundOverlayLayer.Layout.FadeTo(0, 250, Easing.Linear);
            ForegroundOverlayLayer.Layout.IsVisible = false;
            ForegroundOverlayLayer.Layout.IsEnabled = false;
            ParentContainer.Layout.LowerChild(ForegroundOverlayLayer.Layout);
            ForegroundOverlayLayer.Deactivate();
            ForegroundOverlayLayer.Layout.BackgroundColor = Color.Transparent;
            App.IsBusy = false;
            return true;
        }

        public void SetNextPage(int pageId)
        {
            Helpers.Pages.SetNext(pageId);
        }

        public void SetLastPage(int pageId)
        {
            Helpers.Pages.SetLast(Helpers.Pages.GetCurrentPageId());
        }

        public async Task<bool> UpdatePage(int pageID)
        {
            await Helpers.Pages.GetPageById(pageID).Update();
            return true;
        }

        public async Task<bool> UpdatePage()
        {
            await Helpers.Pages.GetCurrent().Update();
            return true;
        }

        public async Task<bool> ReloadPage(int pageID)
        {
            Helpers.Pages.TransitionAction = (int)Helpers.Pages.TransitionActions.Next;
            await GoToPage(pageID);
            return true;
        }

        public async Task<bool> GoToNextPage()
        {
            //Helpers.Pages.SetLast((int)Helpers.Pages.CurrentPage);
            Helpers.Pages.TransitionAction = (int)Helpers.Pages.TransitionActions.Next;
            await GoToPage(Helpers.Pages.GetNextPageId());
            return true;
        }

        public async Task<bool> GoToLastPage()
        {
            Helpers.Pages.TransitionAction = (int)Helpers.Pages.TransitionActions.Last;
            await GoToPage(Helpers.Pages.GetLastPageId());
            return true;
        }

        public async Task<bool> GoToPage()
        {
            //Helpers.Pages.SetLast((int)Helpers.Pages.CurrentPage);
            await GoToPage(Helpers.Pages.GetNextPageId());
            return true;
        }

        public async Task<bool> GoToPage(int pageIndex)
        {
            // specific to the menu page
            //if (Helpers.Pages.GetCurrent().Id == (int)AppSettings.PageNames.TEMenuPage)
            //{
            //    GoToLastPage();
            //    return;
            //}


            await HideMenu();
            await HidePanel();
            await HideForeground();
            await App.ShowLoading();

            // set the target page
            Pages.Page CurrentPage = Helpers.Pages.GetCurrent();
            Pages.Page NewPage = Helpers.Pages.GetPageById(pageIndex);

            Helpers.Pages.SetLast(CurrentPage.Id);

            //await NewPage.DebugUpdate(5000);
            //Helpers.Pages.TransitionDirection = (int)Helpers.Pages.TransitionDirections.Vertical;

            if (CurrentPage.Id != (int)AppSettings.PageNames.Menu)
            {
                Helpers.Pages.PageBeforeMenu = CurrentPage.Id;
            }

            Console.WriteLine("Current page: " + CurrentPage.Id);
            Console.WriteLine("Next page: " + NewPage.Id);
            Console.WriteLine("Going to page: " + pageIndex);

            if ((CurrentPage.Id == NewPage.Id) && NewPage.Id == (int)AppSettings.PageNames.Menu)
            {
                NewPage = Helpers.Pages.GetPageById((int)Helpers.Pages.PageBeforeMenu);
            }

            CurrentPage.TransitionOutType = (int)Helpers.Pages.TransitionTypes.FadeOut;
            NewPage.TransitionInType = (int)Helpers.Pages.TransitionTypes.FadeIn;

            Console.WriteLine("Current page: " + CurrentPage.Id);
            Console.WriteLine("Next page: " + NewPage.Id);
            Console.WriteLine("Going to page: " + pageIndex);

            if (CurrentPage.Id == NewPage.Id)
            {
                Console.WriteLine("ALREADY ON THIS PAGE - Next");
                ContentLayer.Layout.Children.Remove(CurrentPage.GetContent());
                await CurrentPage.Update();
                ContentLayer.Layout.Children.Add(CurrentPage.GetContent());
                await HideLoading();
                return false;
            }

            try
            {
                Helpers.Pages.GoTo(pageIndex);
                await CurrentPage.Update();
                await NewPage.Update();
                await NewPage.PositionPage();

                // add the page to the layout, for presentation
                ContentLayer.Layout.Children.Add(NewPage.GetContent());
                //ContentLayer.Layout.LowerChild(CurrentPage.GetContent());

                await Task.WhenAll(
                        CurrentPage.TransitionOut(),
                        NewPage.TransitionIn()
                    );

                ContentLayer.Layout.Children.Remove(CurrentPage.GetContent());

                Helpers.Pages.SetCurrent(pageIndex);

            }
            catch (Exception e)
            {
                Console.WriteLine("Problem going to next page");
                return false;
            }

            if (pageIndex == (int)AppSettings.PageNames.Landing)
            {
                MainHeader.SetLargeIcon();
            }
            await App.HideLoading();
            return true;
        }
    }
}
