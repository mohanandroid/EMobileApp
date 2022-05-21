using System;
using eCups.Branding;
using eCups.Components.Fields;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Components.Composites
{
    public class LabelEntry : e.ActiveComponent
    {

        public Label TitleLabel;
        public CLabel EntryLabel;
        public Label EditLabel;
        Label ShowLabel;

        public bool passDisplay = false;

        string text;

        public LabelEntry(string titleText, string labelText, bool hidden)
        {
            text = labelText;
            this.Content = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(Units.ScreenWidth10Percent, 0),
                //MinimumHeightRequest = 30,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(70, GridUnitType.Absolute)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto)}
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}
                }
            };

            TitleLabel = new Label
            {
                Text = titleText,
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 14,
                TextColor = Color.FromHex(Colors.EC_GREEN_1),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
            };

            EntryLabel = new CLabel
            {
                Text = labelText,
                FontFamily = Fonts.GetRegularFont(),
                FontSize = 14,
                TextColor = Color.FromHex(Colors.EC_GREEN_2),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };

            EditLabel = new Label
            {
                Text = "Edit",
                FontFamily = Fonts.GetRegularFont(),
                TextDecorations = TextDecorations.Underline,
                FontSize = 12,
                TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };

            Entry entry = new Entry
            {
                Text = labelText,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Transparent,
                PlaceholderColor = Color.Transparent,
                Opacity = 0,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };

            entry.TextChanged += Entry_TextChanged;
            entry.Unfocused += Entry_Unfocused;

            EditLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
              {
                  entry.Focus();
              })
            });

            this.Content.Children.Add(TitleLabel, 0, 0);
            this.Content.Children.Add(EntryLabel, 1, 0);
            this.Content.Children.Add(entry, 1, 0);
            this.Content.Children.Add(EditLabel, 2, 0);

            if (hidden)
            {
                passDisplay = true;

                EntryLabel.SetPassword(passDisplay);

                ShowLabel = new Label
                {
                    Text = "show",
                    FontFamily = Fonts.GetRegularFont(),
                    TextDecorations = TextDecorations.Underline,
                    FontSize = 12,
                    TextColor = Color.Gray,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(0, 0, 25, 0)
                };

                this.Content.Children.Remove(EditLabel);

                this.Content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                this.Content.Children.Add(EditLabel, 3, 0);
                this.Content.Children.Add(ShowLabel, 2, 0);
            }
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            //Update User Profile
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            EntryLabel.Text = $"{entry.Text}";
        }
    }
}
