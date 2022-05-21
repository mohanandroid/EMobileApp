using System;
using System.Threading.Tasks;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.e.Labels
{
    public class ActiveLabel : e.ActiveComponent
    {
        public Label Label { get; set; }
        public string Title { get; set; }
        public int FontSize { get; set; }

        public ActiveLabel(string text, int fontSize, FontName fontName, Color backgroundColor, Color textColor, Models.Action action)
        {
            this.Title = text;
            this.DefaultAction = action;

            this.Content = new Grid
            {

            };


            Label = new Label
            {
                FontFamily = eCups.Helpers.Fonts.GetFont(fontName),
                Text = text,
                FontSize = fontSize,
                BackgroundColor = backgroundColor,
                TextColor = textColor,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };


            if (this.DefaultAction != null)
            {
                this.Content.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await DefaultAction.Execute();
                            });
                        })
                    }
                );
            }
            Content.Children.Add(Label);
        }

        public override async Task<bool> Update()
        {
            await Task.Delay(50);
            Console.WriteLine("Updating Standard Label : " + this.Title);
            return true;
        }
    }
}
