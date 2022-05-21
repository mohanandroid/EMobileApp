using System;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.e.Labels
{
    public class StaticLabel
    {
        public Label Content;

        public StaticLabel(string text)
        {
            this.Content = new Label
            {
                Text = text,
                FontFamily = Fonts.GetRegularFont()
            };
        }

        public void RightAlign()
        {
            Content.HorizontalOptions = LayoutOptions.EndAndExpand;
            Content.HorizontalTextAlignment = TextAlignment.End;

            Content.VerticalOptions = LayoutOptions.CenterAndExpand;
            Content.VerticalTextAlignment = TextAlignment.Center;
        }

        public void LeftAlign()
        {
            Content.HorizontalOptions = LayoutOptions.StartAndExpand;
            Content.HorizontalTextAlignment = TextAlignment.Start;

            Content.VerticalOptions = LayoutOptions.CenterAndExpand;
            Content.VerticalTextAlignment = TextAlignment.Center;
        }

        public void CenterAlign()
        {
            Content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            Content.HorizontalTextAlignment = TextAlignment.Center;

            Content.VerticalOptions = LayoutOptions.CenterAndExpand;
            Content.VerticalTextAlignment = TextAlignment.Center;
        }
    }
}
