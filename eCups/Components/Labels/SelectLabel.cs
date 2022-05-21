using System;
using eCups.e.Images;
using eCups.e.Labels;
using Xamarin.Forms;

namespace eCups.e.Composites
{
    public class SelectLabel
    {
        public Grid Content { get; set; }
        public StackLayout Container { get; set; }

        public Color UncheckedColor { get; set; }
        public Color CheckedColor { get; set; }
        
        public StaticLabel Title { get; set; }

        public bool IsChecked { get; set; }

        public SelectLabel(string title, Color checkedColor, Color uncheckedColor, int width, int height, bool isChecked)
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

            CheckedColor = checkedColor;
            UncheckedColor = uncheckedColor;

            Title = new StaticLabel(title);
            Title.Content.TextColor = UncheckedColor;
            
            IsChecked = isChecked;

            if (IsChecked)
            {
                Title.Content.TextColor = CheckedColor;
            }

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

        
        public void Toggle()
        {
            IsChecked = !IsChecked;

            if (IsChecked)
            {
                Title.Content.TextColor = CheckedColor;
            }
            else
            {
                Title.Content.TextColor = UncheckedColor;
            }
        }
    }
}

