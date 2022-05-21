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
    public class ProgressStepLabel : ActiveComponent
    {
        public int StepNumber { get; set; }

        
        public StaticLabel StepLabel;

        public int LabelWidth;
        

        public ProgressStepLabel(int stepNumber, string stepName)
        {
            LabelWidth = Units.ScreenWidth20Percent;
            StepNumber = stepNumber;

            StepLabel = new StaticLabel(stepName);
            StepLabel.CenterAlign();
            StepLabel.Content.FontSize = Units.FontSizeM;

            Container = new Grid
            {
                WidthRequest = Units.ScreenWidth20Percent,
                HorizontalOptions = LayoutOptions.CenterAndExpand
                //Padding = Units.ScreenWidth5Percent
            };
            Content = new Grid
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            Container.Children.Add(StepLabel.Content, 0, 0);

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
            StepLabel.Content.TextColor = color;
        }
    }


}
