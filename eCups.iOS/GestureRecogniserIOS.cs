using Foundation;
using eCups.iOS;
using eCups.Tools;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Gestures), typeof(GesturesRenderer))]

namespace eCups.iOS
{
    public class GesturesRenderer : ViewRenderer
    {
        public new static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            var mobiusGesture = (Gestures)e.NewElement;

            var tapGestureRecognizer = new TapGestureRecognizer
            {
                OnTouchesBegan = (x, y) => mobiusGesture.OnTouchBegan(x, y),
                OnTouchesEnded = (x, y) => mobiusGesture.OnTouchEnded(x, y),
                OnTap = (x, y) => mobiusGesture.OnTap(x, y)
            };

            var longPressGestureRecognizer = new LongPressGestureRecognizer(() => mobiusGesture.OnLongTap())
            {
                OnTouchesBegan = (x, y) => mobiusGesture.OnTouchBegan(x, y),
                OnTouchesEnded = (x, y) => mobiusGesture.OnTouchEnded(x, y)
            };

            #region SwipeGestureRecognizer

            var swipeLeftGestureRecognizer = new SwipeGestureRecognizer(() => mobiusGesture.OnSwipeLeft())
            {
                Direction = UISwipeGestureRecognizerDirection.Left,
                OnTouchesBegan = (x, y) => mobiusGesture.OnTouchBegan(x, y),
                OnTouchesEnded = (x, y) => mobiusGesture.OnTouchEnded(x, y)
            };

            var swipeRightGestureRecognizer = new SwipeGestureRecognizer(() => mobiusGesture.OnSwipeRight())
            {
                Direction = UISwipeGestureRecognizerDirection.Right,
                OnTouchesBegan = (x, y) => mobiusGesture.OnTouchBegan(x, y),
                OnTouchesEnded = (x, y) => mobiusGesture.OnTouchEnded(x, y)
            };

            var swipeUpGestureRecognizer = new SwipeGestureRecognizer(() => mobiusGesture.OnSwipeUp())
            {
                Direction = UISwipeGestureRecognizerDirection.Up,
                OnTouchesBegan = (x, y) => mobiusGesture.OnTouchBegan(x, y),
                OnTouchesEnded = (x, y) => mobiusGesture.OnTouchEnded(x, y)
            };

            var swipeDownGestureRecognizer = new SwipeGestureRecognizer(() => mobiusGesture.OnSwipeDown())
            {
                Direction = UISwipeGestureRecognizerDirection.Down,
                OnTouchesBegan = (x, y) => mobiusGesture.OnTouchBegan(x, y),
                OnTouchesEnded = (x, y) => mobiusGesture.OnTouchEnded(x, y)
            };

            #endregion

            #region DragGestureRecognizer

            var dragGestureRecognizer = new DragGestureRecognizer
            {
                OnTouchesBegan = (x, y) => mobiusGesture.OnTouchBegan(x, y),
                OnTouchesEnded = (x, y) => mobiusGesture.OnTouchEnded(x, y),
                OnDrag = (x, y) => mobiusGesture.OnDrag(x, y)
            };

            if (mobiusGesture != null)
            {

                if ((mobiusGesture.SupportGestures & Gestures.GestureType.gtSwipeLeft) != 0)
                    dragGestureRecognizer.RequireGestureRecognizerToFail(swipeLeftGestureRecognizer);
                if ((mobiusGesture.SupportGestures & Gestures.GestureType.gtSwipeRight) != 0)
                    dragGestureRecognizer.RequireGestureRecognizerToFail(swipeRightGestureRecognizer);
                if ((mobiusGesture.SupportGestures & Gestures.GestureType.gtSwipeUp) != 0)
                    dragGestureRecognizer.RequireGestureRecognizerToFail(swipeUpGestureRecognizer);
                if ((mobiusGesture.SupportGestures & Gestures.GestureType.gtSwipeDown) != 0)
                    dragGestureRecognizer.RequireGestureRecognizerToFail(swipeDownGestureRecognizer);
            }

            #endregion

            if (e.NewElement == null)
            {
                RemoveGestureRecognizer(tapGestureRecognizer);
                RemoveGestureRecognizer(longPressGestureRecognizer);
                RemoveGestureRecognizer(swipeLeftGestureRecognizer);
                RemoveGestureRecognizer(swipeRightGestureRecognizer);
                RemoveGestureRecognizer(swipeUpGestureRecognizer);
                RemoveGestureRecognizer(swipeDownGestureRecognizer);
                RemoveGestureRecognizer(dragGestureRecognizer);
            }

            if (e.OldElement == null)
            {
                AddGestureRecognizer(tapGestureRecognizer);
                AddGestureRecognizer(longPressGestureRecognizer);
                AddGestureRecognizer(swipeLeftGestureRecognizer);
                AddGestureRecognizer(swipeRightGestureRecognizer);
                AddGestureRecognizer(swipeUpGestureRecognizer);
                AddGestureRecognizer(swipeDownGestureRecognizer);
                AddGestureRecognizer(dragGestureRecognizer);
            }
        }

        private class TapGestureRecognizer : UITapGestureRecognizer
        {
            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                }

                OnTouchesBegan?.Invoke(positionX, positionY);
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (OnTap != null && touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                    OnTap(positionX, positionY);
                }

                OnTouchesEnded?.Invoke(positionX, positionY);
            }

            public override void TouchesCancelled(NSSet touches, UIEvent evt)
            {
                base.TouchesCancelled(touches, evt);

                OnTouchesEnded?.Invoke(-1, -1);
            }

            public Action<double, double> OnTap;
            public Action<double, double> OnTouchesBegan;
            public Action<double, double> OnTouchesEnded;
        }

        private class LongPressGestureRecognizer : UILongPressGestureRecognizer
        {
            public LongPressGestureRecognizer(Action action)
                : base(action)
            {
            }

            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                }

                OnTouchesBegan?.Invoke(positionX, positionY);
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                }

                OnTouchesEnded?.Invoke(positionX, positionY);
            }

            public override void TouchesCancelled(NSSet touches, UIEvent evt)
            {
                base.TouchesCancelled(touches, evt);

                OnTouchesEnded?.Invoke(-1, -1);
            }

            public Action<double, double> OnTouchesBegan;
            public Action<double, double> OnTouchesEnded;
        }

        private class SwipeGestureRecognizer : UISwipeGestureRecognizer
        {
            public SwipeGestureRecognizer(Action action)
                : base(action)
            {
            }

            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                }

                OnTouchesBegan?.Invoke(positionX, positionY);
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                }

                OnTouchesEnded?.Invoke(positionX, positionY);
            }

            public override void TouchesCancelled(NSSet touches, UIEvent evt)
            {
                base.TouchesCancelled(touches, evt);

                OnTouchesEnded?.Invoke(-1, -1);
            }

            public Action<double, double> OnTouchesBegan;
            public Action<double, double> OnTouchesEnded;
        }

        private class DragGestureRecognizer : UIPanGestureRecognizer
        {
            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                }

                OnTouchesBegan?.Invoke(positionX, positionY);
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                double positionX = -1;
                double positionY = -1;

                if (touches.AnyObject is UITouch touch)
                {
                    positionX = touch.LocationInView(View).X;
                    positionY = touch.LocationInView(View).Y;
                }

                OnTouchesEnded?.Invoke(positionX, positionY);
            }

            public override void TouchesMoved(NSSet touches, UIEvent evt)
            {
                base.TouchesMoved(touches, evt);

                if (OnDrag != null && touches.AnyObject is UITouch touch)
                {
                    var offsetX = touch.PreviousLocationInView(View).X - (double)touch.LocationInView(View).X;
                    var offsetY = touch.PreviousLocationInView(View).Y - (double)touch.LocationInView(View).Y;
                    OnDrag(-offsetX, -offsetY);
                }
            }

            public override void TouchesCancelled(NSSet touches, UIEvent evt)
            {
                base.TouchesCancelled(touches, evt);

                OnTouchesEnded?.Invoke(-1, -1);
            }

            public Action<double, double> OnTouchesBegan;
            public Action<double, double> OnTouchesEnded;
            public Action<double, double> OnDrag;
        }
    }
}