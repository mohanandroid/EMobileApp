using System;
using System.Collections.Generic;
using eCups.Branding;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using FFImageLoading.Transformations;
using Xamarin.Forms;

namespace eCups.e.Composites
{
    public class ProgressStepIcon : ActiveComponent
    {
        public int StepNumber { get; set; }

        public StaticImage StepImage;

        public int IconWidth;

        public ProgressStepIcon(int stepNumber, string stepName)
        {
            IconWidth = 32;
            StepNumber = stepNumber;

            StepImage = new StaticImage("progress_circle_small_inner.png", IconWidth, null);
            StepImage.Content.HeightRequest = IconWidth;
            StepImage.Content.VerticalOptions = LayoutOptions.CenterAndExpand;

            Container = new Grid
            {
                WidthRequest = Units.ScreenWidth10Percent,
                HorizontalOptions = LayoutOptions.CenterAndExpand
                //Padding = Units.ScreenWidth5Percent
            };
            Content = new Grid
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            Container.Children.Add(StepImage.Content, 0, 0);
            
            Tint(Color.White);

            Content.Children.Add(Container);
        }

        public void SetComplete()
        {
            Tint(Color.FromHex(Colors.EC_BRIGHT_GREEN));
        }

        public void SetIncomplete()
        {
            Tint(Color.White);
        }

        public void Tint(Color color)
        {
            TintTransformation colorTint = new TintTransformation
            {
                HexColor = (string)color.ToHex(),
                EnableSolidColor = true

            };

            Container.Children.Clear();
            
            StepImage = new StaticImage("progress_circle_small_inner.png", IconWidth, null);
            StepImage.Content.Transformations = new List<FFImageLoading.Work.ITransformation>();
            StepImage.Content.Transformations.Add(colorTint);
            StepImage.Content.Opacity = 1;

            Container.Children.Add(StepImage.Content, 0, 0);
        }
    }


}
