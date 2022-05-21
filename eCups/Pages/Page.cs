using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using eCups.Branding;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Renderers;
using MagicGradients;
using Refractored.XamForms.PullToRefresh;
using eCups.Services;
using Xamarin.Forms;
using eCups.e.Buttons;
using eCups.Components.Buttons;
using eCups.Helpers.Custom;

namespace eCups.Pages
{
    public class Page// : INotifyPropertyChanged
    {
        public int Id;
        public string Name;
        public Grid PageContent;
        public int ContentHeight;
        public string BackgroundImageSource;
        public StaticImage BackgroundImage;
        public View BackgroundView;
        public string DecorImageSource;
        public StaticImage DecorImage;
        public int TransitionInType;
        public int TransitionOutType;
        public uint TransitionSpeed;
        public bool IsScrollable;
        public bool IsRefreshable;
        public bool NeedsRefreshing;
        public CScrollView contentScrollView;
        public Grid Content;
        public RefreshView RefreshView;
        public ICommand RefreshCommand { get; }

        // overrides for page specific layouts
        public bool HasHeader;
        public bool HasFooter;
        public bool HasSubHeader;
        public bool HasNavHeader;

        //Timer stuff

        public StoppableTimer PageTimer;
        public bool TimerRunning;

        public Page()
        {
            TransitionSpeed = (uint)AppSettings.TransitionVeryFast;
            ContentHeight = Units.ScreenHeight;
            IsScrollable = true;
            IsRefreshable = false;
            HasFooter = false;
            HasHeader = false;
            HasSubHeader = false;
            HasNavHeader = false;
            NeedsRefreshing = true;
            RefreshCommand = new Command(ExecuteRefreshCommand);

            PageTimer = new StoppableTimer(TimeSpan.FromSeconds(2), TimedUpdate, true);
            TimerRunning = false;
            PageTimer.Stop();
        }


        public virtual void TimedUpdate()
        {
            // override this however you like, per page
        }

        bool isRefreshing;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                //OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        //private void OnPropertyChanged(string v)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual void ExecuteRefreshCommand()
        {
            //Items.Clear();
            //Items.Add(new Item { Text = "Refreshed Data", Description = "Whoa!" });
            App.ShowAlert("Finished updating");
            // Stop refreshing
            IsRefreshing = false;
            RefreshView.IsRefreshing = false;
        }

        public virtual Grid GetContent()
        {
            if (PageContent == null)
            {
                Create();
            }

            if (IsScrollable)
            {
                Content = new Grid
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                };

                contentScrollView = new CScrollView
                {
                    Content = PageContent,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    MinimumHeightRequest = Units.ThirdScreenHeight,
                    BackgroundColor = Color.Transparent,
                    AnchorY = 0
                };

                if (IsRefreshable)
                {
                    RefreshView = new RefreshView
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Content = contentScrollView,
                        RefreshColor = Color.FromHex(Colors.REFRESH_SPINNER_COLOUR),
                        Command = RefreshCommand
                    };
                    Content.Children.Add(RefreshView);
                }
                else
                {
                    Content.Children.Add(contentScrollView);
                }

                return Content;
            }
            else
            {
                return PageContent;
            }
        }

        public virtual void AddBackgroundImage(string backgroundImageSource)
        {
            BackgroundImageSource = backgroundImageSource;
            BackgroundImage = new StaticImage(
            BackgroundImageSource,
            Units.ScreenWidth,
            Units.ScreenHeight,
            null);
            BackgroundImage.Content.Aspect = Aspect.Fill;
            PageContent.Children.Add(BackgroundImage.Content, 0, 0);
        }

        public virtual void AddBackgroundView(View view)
        {
            BackgroundView = view;
            PageContent.Children.Add(BackgroundView, 0, 0);
        }

        public virtual void AddBackgroundView(View view, Thickness thickness)
        {
            BackgroundView = view;
            BackgroundView.Margin = thickness;
            PageContent.Children.Add(BackgroundView, 0, 0);
        }

        public virtual void AddDecor(string decorImageSource, int height, LayoutOptions position)
        {
            DecorImageSource = decorImageSource;
            DecorImage = new StaticImage(
            DecorImageSource,
            Units.ScreenWidth,
            height,
            null);
            DecorImage.Content.VerticalOptions = position;
            DecorImage.Content.Aspect = Aspect.Fill;
            DecorImage.Content.InputTransparent = true;
            PageContent.Children.Add(DecorImage.Content, 0, 0);
        }

        public virtual void AddDecor(string decorImageSource, int height, LayoutOptions position, Thickness thickness)
        {
            DecorImageSource = decorImageSource;
            DecorImage = new StaticImage(
            DecorImageSource,
            Units.ScreenWidth,
            height,
            null);
            DecorImage.Content.VerticalOptions = position;
            DecorImage.Content.Aspect = Aspect.Fill;
            DecorImage.Content.Margin = thickness;
            DecorImage.Content.InputTransparent = true;
            PageContent.Children.Add(DecorImage.Content, 0, 0);
        }


        public virtual void AddBackgroundGradient(string start, string end)
        {
            GradientElements<GradientStop> gradientStops = new GradientElements<GradientStop>();
            gradientStops.Add(new GradientStop { Color = Color.FromHex(start) });
            gradientStops.Add(new GradientStop { Color = Color.FromHex(end) });


            GradientView gradient = new GradientView
            {
                GradientSource = new LinearGradient
                {
                    Angle = 0,
                    Stops = gradientStops
                },
                HeightRequest = Units.ScreenHeight,
                WidthRequest = Units.ScreenWidth,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };
            PageContent.Children.Add(gradient, 0, 0); 
        }

        public virtual void AddBackgroundGradient(GradientElements<GradientStop> gradientStops)
        {
            GradientView gradient = new GradientView
            {
                GradientSource = new LinearGradient
                {
                    Angle = 0,
                    Stops = gradientStops
                },
                HeightRequest = Units.ScreenHeight,
                WidthRequest = Units.ScreenWidth,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };
            PageContent.Children.Add(gradient, 0, 0);
        }

        public void AddMenuBar()
        {
            Grid menu = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {
                        Width = Units.ScreenWidth40Percent
                    },
                    new ColumnDefinition
                    {
                        Width = Units.ScreenWidth20Percent
                    },
                    new ColumnDefinition
                    {
                        Width = Units.ScreenWidth40Percent
                    }
                },
                VerticalOptions = LayoutOptions.End
            };

            BoxView bar = new BoxView
            {
                Color = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                HeightRequest = 45,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0,5,0,-5)
            };

            //MenuButton menuButton = new MenuButton(Units.ScreenWidth45Percent, Units.ScreenHeight12Percent, Color.Black, Color.Cyan, Color.White, "Menu", "bars.png", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.Menu));
            //MenuButton qrButton = new MenuButton(Units.ScreenWidth45Percent, Units.ScreenHeight12Percent, Color.Black, Color.Cyan, Color.White, "Scan", "camera.png", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner));
            MenuHudButton menuButton = new MenuHudButton("Menu", "bars.png", new Models.Action((int)Actions.ActionName.ShowMenu));
            MenuHudButton qrButton = new MenuHudButton("Scan", "camera.png", new Models.Action((int)Actions.ActionName.GoToPage, (int)AppSettings.PageNames.QRScanner));
            menuButton.SetGradient(1, 0.5f, 0, 0.5f);
            menuButton.Content.Margin = new Thickness(-50, 0, 0, 0);
            menuButton.buttonContents.Margin = new Thickness(15, 10, -15, 10);
            qrButton.Content.Margin = new Thickness(0, 0, -50, 0);
            qrButton.buttonContents.Margin = new Thickness(-15, 10, 15, 10);

            menu.Children.Add(bar, 0, 0);
            menu.Children.Add(menuButton.Content, 0, 0);
            menu.Children.Add(qrButton.Content, 2, 0);

            menu.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Console.WriteLine("Pressed");
                    });
                })
            });

            Grid.SetColumnSpan(bar, menu.ColumnDefinitions.Count);

            PageContent.Children.Add(menu);
        }

        public virtual string GetBackgroundImageSource()
        {
            return BackgroundImageSource;
        }

        public virtual void Create()
        {
            PageContent = new Grid();
        }

        public virtual void Destroy()
        {
            PageContent = null;
        }

        public virtual async Task Update()
        {
            await Task.Delay(10);

            if (this.HasHeader)
            {
                App.ShowHeader();
            }
            else
            {
                App.HideHeader();
            }

            if (this.HasSubHeader)
            {
                App.ShowSubHeader();
            }
            else
            {
                App.HideSubHeader();
            }

            if (this.HasNavHeader)
            {
                App.ShowNavHeader();
            }
            else
            {
                App.HideNavHeader();
            }

            if (this.HasFooter)
            {
                App.ShowFooter();
            }
            else
            {
                App.HideFooter();
            }
        }



        public virtual async Task ReBuild()
        {
            await Task.Delay(10);
        }

        public virtual async Task DebugUpdate(int time)
        {
            await Task.Delay(time);
        }

        public virtual void ResetScrollView()
        {
           
        }

        public virtual async Task TransitionIn()
        {
            Console.WriteLine("Transition: " + TransitionInType + " opacity before " + PageContent.Opacity + " position x " + PageContent.X + " position y " + PageContent.Y);

            switch (TransitionInType)
            {
                case (int)Helpers.Pages.TransitionTypes.FadeIn:
                    await Task.WhenAll(
                        PageContent.FadeTo(1, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.ScaleIn:
                    await Task.WhenAll(
                        PageContent.ScaleTo(1, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.RotateIn:
                    await Task.WhenAll(
                        PageContent.RotateTo(360, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.ScaleAndRotateIn:
                    await Task.WhenAll(
                        PageContent.ScaleTo(1, TransitionSpeed, Easing.Linear),
                        PageContent.RotateTo(360, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.ScaleRotateAndFadeIn:
                    await Task.WhenAll(
                        PageContent.FadeTo(1, TransitionSpeed, Easing.Linear),
                        PageContent.ScaleTo(1, TransitionSpeed, Easing.Linear),
                        PageContent.RotateTo(360, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideInFromLeft:
                case (int)Helpers.Pages.TransitionTypes.SlideInFromRight:
                    await Task.WhenAll(
                        PageContent.TranslateTo(0, 0, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideInFromTop:
                case (int)Helpers.Pages.TransitionTypes.SlideInFromBottom:
                    await Task.WhenAll(
                        PageContent.TranslateTo(0, 0, TransitionSpeed, Easing.Linear)
                    );
                    break;


            }
            Console.WriteLine("Transition: " + TransitionInType + " opacity after " + PageContent.Opacity + " position x " + PageContent.X + " position y " + PageContent.Y);
        }

        public virtual async Task TransitionOut()
        {
            switch (TransitionOutType)
            {

                case (int)Helpers.Pages.TransitionTypes.FadeOut:
                    await Task.WhenAll(
                        PageContent.FadeTo(0, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.ScaleOut:
                    await Task.WhenAll(
                        PageContent.ScaleTo(0, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.RotateOut:
                    await Task.WhenAll(
                        PageContent.RotateTo(-360, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.ScaleAndRotateOut:
                    await Task.WhenAll(
                        PageContent.ScaleTo(0, TransitionSpeed, Easing.Linear),
                        PageContent.RotateTo(-360, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.ScaleRotateAndFadeOut:
                    await Task.WhenAll(
                        PageContent.FadeTo(0, TransitionSpeed, Easing.Linear),
                        PageContent.ScaleTo(0, TransitionSpeed, Easing.Linear),
                        PageContent.RotateTo(-360, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideOutToLeft:
                    await Task.WhenAll(
                        PageContent.TranslateTo(-Units.ScreenWidth, 0, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideOutToRight:
                    await Task.WhenAll(
                        PageContent.TranslateTo(Units.ScreenWidth, 0, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideOutToTop:
                    await Task.WhenAll(
                        PageContent.TranslateTo(0, -Units.ScreenHeight, TransitionSpeed, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideOutToBottom:
                    await Task.WhenAll(
                        PageContent.TranslateTo(0, Units.ScreenHeight, TransitionSpeed, Easing.Linear)
                    );
                    break;
            }
        }

        public virtual async Task PositionPage()
        {
            switch(TransitionInType)
            {
                case (int)Helpers.Pages.TransitionTypes.FadeIn: 
                case (int)Helpers.Pages.TransitionTypes.ScaleIn:
                case (int)Helpers.Pages.TransitionTypes.RotateIn:
                case (int)Helpers.Pages.TransitionTypes.ScaleAndRotateIn:
                case (int)Helpers.Pages.TransitionTypes.ScaleRotateAndFadeIn:
                    await PageContent.TranslateTo(0, 0, 0, Easing.Linear);
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideInFromLeft:
                    await Task.WhenAll(
                        PageContent.TranslateTo(-Units.ScreenWidth, 0, 0, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideInFromRight:
                    await Task.WhenAll(
                       PageContent.TranslateTo(Units.ScreenWidth, 0, 0, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideInFromTop:
                    await Task.WhenAll(
                       PageContent.TranslateTo(0, -Units.ScreenHeight, 0, Easing.Linear)
                    );
                    break;
                case (int)Helpers.Pages.TransitionTypes.SlideInFromBottom:
                    await Task.WhenAll(
                       PageContent.TranslateTo(0,-Units.ScreenHeight, 0, Easing.Linear)
                    );
                    break;
            }
        }

        /*
        public class TestClassThing : INotifyPropertyChanged
        {

            bool canRefresh = true;

            public bool CanRefresh
            {
                get { return canRefresh; }
                set
                {
                    if (canRefresh == value)
                        return;

                    canRefresh = value;
                    OnPropertyChanged("CanRefresh");
                }
            }


            bool isBusy;

            public bool IsBusy
            {
                get { return isBusy; }
                set
                {
                    if (isBusy == value)
                        return;

                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }

            ICommand refreshCommand;

            public ICommand RefreshCommand
            {
                get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
            }

            async Task ExecuteRefreshCommand()
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                //Items.Clear();

                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {

                    //for (int i = 0; i < 100; i++)
                    //    Items.Add(DateTime.Now.AddMinutes(i).ToString("F"));

                    IsBusy = false;

                    //DisplayAlert("Refreshed", "You just refreshed the page! Nice job! Pull to refresh is now disabled", "OK");
                    this.CanRefresh = false;

                    return false;
                });
            }

            #region INotifyPropertyChanged implementation

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/
    }
}
