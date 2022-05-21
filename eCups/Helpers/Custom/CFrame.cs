using System;
using Xamarin.Forms;

namespace eCups.Helpers.Custom
{
    public class CFrame : Frame
    {
        public int BorderWidth { get; set; }

        public Color GradientStartColor { get; set; }
        public Color GradientEndColor { get; set; }
        public float GradientStartX = 0;
        public float GradientEndX = 1;
        public float GradientStartY = 0.5f;
        public float GradientEndY = 0.5f;
    }
}
