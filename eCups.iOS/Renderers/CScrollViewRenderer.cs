using System;
using eCups.Helpers.Custom;
using eCups.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CScrollView), typeof(CScrollViewRenderer))]
namespace eCups.iOS.Renderers
{
    public class CScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            var scroll = (CScrollView)e.NewElement;
            Bounces = scroll.Elastic;
        }
    }
}
