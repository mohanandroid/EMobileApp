using System;
using System.Collections.Generic;
using eCups.Branding;
using eCups.e.Composites;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using Xamarin.Forms;

namespace eCups.Layouts.Custom.Panels
{
    public class ProgressIndicator : StandardLayout
    {
        public StackLayout StepIconsContainer;
        public StackLayout StepLabelsContainer;
        public StackLayout StepsContainer;

        public int CurrentProgress { get; set; }

        public List<ProgressStepIcon> StepIcons;
        List<ProgressStepLabel> StepLabels;

        Grid Path1;
        Grid Path2;

        public ProgressIndicator()
        {
            StepIcons = new List<ProgressStepIcon>();
            StepIcons.Add(new ProgressStepIcon(1, "User Details"));
            StepIcons.Add(new ProgressStepIcon(2, "Add eCup"));
            StepIcons.Add(new ProgressStepIcon(3, "Final Setup"));

            StepLabels = new List<ProgressStepLabel>();
            StepLabels.Add(new ProgressStepLabel(1, "User Details"));
            StepLabels.Add(new ProgressStepLabel(2, "Add eCup"));
            StepLabels.Add(new ProgressStepLabel(3, "Final Setup"));

            StepsContainer = new StackLayout
            {
                WidthRequest = Units.ScreenWidth,
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            StepIconsContainer = new StackLayout
            {
                WidthRequest = Units.ScreenWidth,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(Units.ScreenWidth5Percent, 0)
            };

            StepLabelsContainer = new StackLayout
            {
                WidthRequest = Units.ScreenWidth,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(0, 0)
            };


            Path1 = new Grid
            {
                BackgroundColor = Color.White,
                HeightRequest = 2,
                WidthRequest = Units.ScreenWidth20Percent,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Path2 = new Grid
            {
                BackgroundColor = Color.White,
                HeightRequest = 2,
                WidthRequest = Units.ScreenWidth20Percent,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Content = new Grid { };
            Container = new Grid
            {
                Padding = new Thickness(16, 0)
            };

            StepIconsContainer.Children.Add(StepIcons[0].Content);
            StepIconsContainer.Children.Add(Path1);
            StepIconsContainer.Children.Add(StepIcons[1].Content);
            StepIconsContainer.Children.Add(Path2);
            StepIconsContainer.Children.Add(StepIcons[2].Content);

            StepLabels[0].StepLabel.LeftAlign();
            StepLabels[1].StepLabel.CenterAlign();
            StepLabels[2].StepLabel.RightAlign();

            StepLabelsContainer.Children.Add(StepLabels[0].Content);
            StepLabelsContainer.Children.Add(StepLabels[1].Content);
            StepLabelsContainer.Children.Add(StepLabels[2].Content);

            StepsContainer.Children.Add(StepIconsContainer);
            StepsContainer.Children.Add(StepLabelsContainer);

            Container.Children.Add(StepsContainer);
            Content.Children.Add(Container);

        }

        public void SetProgess(int progress)
        {
            CurrentProgress = progress;

            if (CurrentProgress > StepIcons.Count)
            {
                CurrentProgress = StepIcons.Count;
            }

            for (int i = 0; i < StepIcons.Count; i++)
            {
                StepIcons[i].SetIncomplete();
                StepLabels[i].SetIncomplete();
            }

            StepIcons[CurrentProgress - 1].SetComplete();
            StepLabels[CurrentProgress - 1].SetComplete();

            Path1.BackgroundColor = Color.FromHex(Colors.EC_WHITE);
            Path2.BackgroundColor = Color.FromHex(Colors.EC_WHITE);
        }
    }
}
