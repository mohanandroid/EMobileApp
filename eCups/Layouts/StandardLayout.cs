using System;
using System.Threading.Tasks;
using eCups.Tools;
using Xamarin.Forms;

namespace eCups.Layouts
{
    public class StandardLayout
    {
        public Grid Content;
        public Grid Container;
        public DraggableView DraggableView;

        public int Height { get; set; }
        public int Width { get; set; }
        public uint TransitionTime { get; set; }
        public int TransitionType { get; set; }

        public StandardLayout()
        {

        }

        public void SetWidth(int width)
        {
            Width = width;
            Content.WidthRequest = width;
        }

        public void SetHeight(int height)
        {
            Height = height;
            Content.HeightRequest = height;
        }

        public async Task<bool> Show()
        {
            Content.IsVisible = true;
            switch (TransitionType)
            {
                case (int)AppSettings.TransitionTypes.SlideOutTop:
                case (int)AppSettings.TransitionTypes.SlideOutBottom:
                case (int)AppSettings.TransitionTypes.SlideOutLeft:
                case (int)AppSettings.TransitionTypes.SlideOutRight:
                    await Task.WhenAll(
                        Content.TranslateTo(0, 0, TransitionTime, Easing.Linear),
                        Content.FadeTo(1, TransitionTime, Easing.Linear)
                        );
                    break;
                case (int)AppSettings.TransitionTypes.FadeOut:
                    await Task.WhenAll(
                        Content.FadeTo(1, TransitionTime, Easing.Linear)
                        );
                    break;
            }

            if (DraggableView!=null)
            {
                DraggableView.RestorePositionCommand.Execute(null);
            }
            return true;
        }

        public async Task<bool> Hide()
        {
            switch (TransitionType)
            {
                case (int)AppSettings.TransitionTypes.SlideOutTop:
                    await Task.WhenAll(
                        Content.TranslateTo(0, -Height, TransitionTime, Easing.Linear)
                        );
                    break;
                case (int)AppSettings.TransitionTypes.SlideOutBottom:
                    await Task.WhenAll(
                        Content.TranslateTo(0, -Height, TransitionTime, Easing.Linear)
                        );
                    break;
                case (int)AppSettings.TransitionTypes.SlideOutLeft:
                    await Task.WhenAll(
                        Content.TranslateTo(-Width, 0, TransitionTime, Easing.Linear),
                        Content.FadeTo(0, TransitionTime, Easing.Linear)
                        );
                    break;
                case (int)AppSettings.TransitionTypes.SlideOutRight:
                    await Task.WhenAll(
                        Content.TranslateTo(Width, 0, TransitionTime, Easing.Linear),
                        Content.FadeTo(0, TransitionTime, Easing.Linear)
                        );
                    break;
                case (int)AppSettings.TransitionTypes.FadeOut:
                    await Task.WhenAll(
                        Content.FadeTo(0, TransitionTime, Easing.Linear)
                        );
                    break;
            }
            Content.IsVisible = false;
            return true;
        }
    }
}
