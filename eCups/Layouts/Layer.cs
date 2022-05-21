using System;
using eCups.e.Images;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups
{
    public class Layer
    {
        public Grid Layout { get; set; }
        public StaticImage BackgroundImage;
        public string BackgroundImageSource;

        public Layer()
        {
            Layout = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0
            };
        }

        public Layer(int width, int height)
        {
            Layout = new Grid
            {
                WidthRequest = width,
                HeightRequest = height,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
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
            Layout.Children.Add(BackgroundImage.Content);
        }

        public virtual void SetBackGroundImageAspect(Aspect aspect)
        {
            //BackgroundImage.Content.Aspect = aspect;
        }

        public void Activate()
        {
            Layout.IsEnabled = true;
            Layout.IsVisible = true;
        }

        public void Deactivate()
        {
            Layout.IsEnabled = false;
            Layout.IsVisible = false;
        }

        public bool IsActive()
        {
            return Layout.IsEnabled;
        }

    }
}
