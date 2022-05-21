using System;
using eCups.Branding;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.e.Labels
{
    public class ComplexLabel
    {
        public Grid Content;
        public StaticLabel Title { get; set; }
        public StaticLabel Text { get; set; }
        public StaticLabel Show { get; set; }
        public StaticLabel Edit { get; set; }

        public ComplexLabel(string title, string text, bool isEditable, bool isPassword)
        {
            Content = new Grid {  };

            Content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Units.ScreenWidth20Percent) });
            Content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Units.ScreenWidth40Percent) });
            Content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });
            Content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            Title = new StaticLabel(title);
            Title.Content.TextColor = Color.Black;
            Title.Content.FontSize = Units.FontSizeML;
            Title.LeftAlign();

            Text = new StaticLabel(text);
            Text.Content.TextColor = Color.FromHex(Colors.EC_GREEN_2);
            Text.Content.FontSize = Units.FontSizeML;
            Text.LeftAlign();

            Show = new StaticLabel("Show");
            Show.Content.TextColor = Color.Gray;
            Show.Content.FontSize = Units.FontSizeM;
            Show.Content.TextDecorations = TextDecorations.Underline;
            Show.LeftAlign();

            Edit = new StaticLabel("Edit");
            Edit.Content.TextColor = Color.FromHex(Colors.EC_BRIGHT_GREEN);
            Edit.Content.FontSize = Units.FontSizeM;
            Edit.Content.TextDecorations = TextDecorations.Underline;
            Edit.RightAlign();

            Content.Children.Add(Title.Content, 0, 0);
            Content.Children.Add(Text.Content, 1, 0);
            if (isPassword)
            {
                Content.Children.Add(Show.Content, 2, 0);
            }

            if (isEditable)
            {
                Content.Children.Add(Edit.Content, 3, 0);
            }
        }
    }
}
