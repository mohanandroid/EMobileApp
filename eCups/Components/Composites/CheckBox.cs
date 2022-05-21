using System;
using eCups.e.Images;
using eCups.e.Labels;
using Xamarin.Forms;

namespace eCups.e.Composites
{
    public class CheckBox
    {
        public Grid Content { get; set; }
        public StackLayout Container { get; set; }

        public string IconUncheckedImageSource { get; set; }
        public string IconCheckedImageSource { get; set; }
        public StaticImage Icon { get; set; }

        public StaticLabel Title { get; set; }

        public bool IsChecked { get; set; }

        public CheckBox(string title, string iconCheckedImageSource, string iconUncheckedImageSource, int width, int height, bool isChecked)
        {
            Content = new Grid
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = width,
                HeightRequest = height
            };

            Container = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            Title = new StaticLabel(title);
            IconCheckedImageSource = iconCheckedImageSource;
            IconUncheckedImageSource = iconUncheckedImageSource;

            Icon = new StaticImage(IconUncheckedImageSource, height, height, null);

            IsChecked = isChecked;

            if (IsChecked)
            {
                Icon.Content.Source = IconCheckedImageSource;
            }

            Container.Children.Add(Icon.Content);
            Container.Children.Add(Title.Content);

            Container.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                Toggle();
                            });
                        })
                    }
                );

            Content.Children.Add(Container);
        }

        public void SetCheckBoxRight()
        {

        }

        public void SetCheckboxLeft()
        {

        }

        public void Toggle()
        {
            IsChecked = !IsChecked;

            if (IsChecked)
            {
                Icon.Content.Source = IconCheckedImageSource;
            }
            else
            {
                Icon.Content.Source = IconUncheckedImageSource;
            }
        }
    }
}
