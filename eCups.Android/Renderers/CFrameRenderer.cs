using System;
using Xamarin.Forms;
using eCups.Droid.Renderers;
using eCups.Helpers.Custom;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CFrame), typeof(CFrameRenderer))]
namespace eCups.Droid.Renderers
{
    public class CFrameRenderer : FrameRenderer
    {
        public CFrameRenderer(Context context) : base(context)
        {
        }


        public override void Draw(Canvas canvas)
        {
           base.Draw(canvas);

            var frame = Element as CFrame;

            var BorderPaint = new Paint()
            {
                AntiAlias = true,
                StrokeWidth = frame.BorderWidth * 2,
                Color = frame.BorderColor.ToAndroid()
            };
            BorderPaint.SetStyle(Paint.Style.Stroke);

            float gradientStartX = 0;
            float gradientStartY = 0;
            float gradientEndX = 0;
            float gradientEndY = 0;
            try { gradientStartX = Width * frame.GradientStartX; } catch { }
            try { gradientStartY = Height * frame.GradientStartY; } catch { }
            try { gradientEndX = Width * frame.GradientEndX; } catch { }
            try { gradientEndY = Height * frame.GradientEndY; } catch { }

            var gradient = new LinearGradient(gradientStartX, gradientStartY, gradientEndX, gradientEndY, frame.GradientStartColor.ToAndroid(), frame.GradientEndColor.ToAndroid(), Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Paint()
            {
                Dither = true,
                AntiAlias = true
            };
            paint.SetShader(gradient);

            Rect OldBounds = new Rect();
            canvas.GetClipBounds(OldBounds);

            RectF Bounds = new RectF();
            Bounds.Set(OldBounds);

            //If you want to change the bounds of the outline use the below
            //RectF BorderBounds = new RectF();
            //BorderBounds.Set(OldBounds);
            //BorderBounds.Top += (int)BorderPaint.StrokeWidth * 1.5f;
            //BorderBounds.Bottom -= (int)BorderPaint.StrokeWidth * 1.5f;
            //BorderBounds.Left += (int)BorderPaint.StrokeWidth * 1.5f;
            //BorderBounds.Right -= (int)BorderPaint.StrokeWidth * 1.5f;

            canvas.DrawRoundRect(Bounds, 100f, 100f, paint);

            canvas.DrawRoundRect(Bounds, 100f, 100f, BorderPaint);

        }
    }
}
