using System;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using eCups.Helpers.Custom;
using eCups.iOS.Renderers;
using CoreAnimation;
using System.Drawing;

[assembly: ExportRenderer(typeof(CFrame), typeof(CFrameRenderer))]
namespace eCups.iOS.Renderers
{
    public class CFrameRenderer : FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            CFrame frame = (CFrame)this.Element;
            CGColor startColor = frame.GradientStartColor.ToCGColor();

            CGColor endColor = frame.GradientEndColor.ToCGColor();

            var gradientLayer = new CAGradientLayer();

            //Allows you to set a gradient in any direction using points between 0.0 and 1.0 for X and Y
            gradientLayer.Frame = rect;
            gradientLayer.StartPoint = new PointF { X = frame.GradientStartX, Y = frame.GradientStartY };
            gradientLayer.EndPoint = new PointF { X = frame.GradientEndX, Y = frame.GradientEndY };
            gradientLayer.Colors = new CGColor[] { startColor, endColor };

            if (frame.BorderWidth > 0)
            {
                this.Layer.BorderColor = frame.BorderColor.ToCGColor();
                this.Layer.BorderWidth = frame.BorderWidth;
            }

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}
