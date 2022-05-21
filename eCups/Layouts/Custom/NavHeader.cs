using System;
using System.Threading.Tasks;
using eCups.Branding;
using eCups.e.Buttons;
using eCups.e.Images;
using eCups.Helpers;
using eCups.Tools;
using Xamarin.Forms;

namespace eCups.Layouts.Custom
{
    public class NavHeader : StandardLayout
    {
        ActiveImage Logo;
        IconButton MenuButton;

        public NavHeader()
        {
            Height = Units.TapSizeXS;
            Width = Units.ScreenWidth;
            TransitionTime = 150;
            TransitionType = (int)AppSettings.TransitionTypes.SlideOutRight;

            Content = new Grid();

            Container = new Grid
            {
                WidthRequest = Width,
                HeightRequest = Height,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(Units.ScreenHeight5Percent, 0, Units.ScreenWidth10Percent, 1),
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.FromHex(Colors.BH_PINK)
            };

            //string test = App.GetData((int)AppSettings.RequestIds.GetSomething);

            Container.Children.Add(new Label { Text = "test", TextColor = Color.White, VerticalTextAlignment = TextAlignment.Center });

            var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            leftSwipeGesture.Swiped += OnSwiped;
            var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            rightSwipeGesture.Swiped += OnSwiped;
            var upSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Up };
            upSwipeGesture.Swiped += OnSwiped;
            var downSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Down };
            downSwipeGesture.Swiped += OnSwiped;


            DraggableView = new DraggableView
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = Content.Width,
                HeightRequest = Content.Height,
                DragMode = DragMode.Touch,
                DragDirection = DragDirectionType.Horizontal
            };

            DraggableView.DragStart += delegate (object sender, EventArgs e) { DragStarted(sender, e); };
            DraggableView.DragEnd += delegate (object sender, EventArgs e) { DragEnded(sender, e); };

            DraggableView.GestureRecognizers.Add(leftSwipeGesture);
            DraggableView.GestureRecognizers.Add(rightSwipeGesture);
            DraggableView.GestureRecognizers.Add(upSwipeGesture);
            DraggableView.GestureRecognizers.Add(downSwipeGesture);

            DraggableView.Content = Container;

            Content.Children.Add(DraggableView, 0, 0);


        }

        void DragStarted(object sender, EventArgs e)
        {
            Container.BackgroundColor = Color.FromHex(Colors.BH_PURPLE);
        }

        void DragEnded(object sender, EventArgs e)
        {

            Console.WriteLine("SCREEN WIDTH: " + Units.ScreenWidth);
            Console.WriteLine("SCREEN WIDTH: " + Units.PixelWidth);
            Console.WriteLine("X: " + DraggableView.MovedX);

            Container.BackgroundColor = Color.FromHex(Colors.BH_PINK);
            if (DraggableView.MovedX > Units.PixelWidth / 2)
            {
                App.HideNavHeader();
            }
            else
            {
                DraggableView.RestorePositionCommand.Execute(null);

                // perform tap command

            }
        }

        void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    // Handle the swipe
                    //App.HideNavHeader();
                    Console.WriteLine("SWIPE LEFT");
                    break;
                case SwipeDirection.Right:
                    // Handle the swipe
                    Console.WriteLine("SWIPE RIGHT");
                    break;
                case SwipeDirection.Up:
                    // Handle the swipe
                    Console.WriteLine("SWIPE UP");
                    break;
                case SwipeDirection.Down:
                    // Handle the swipe
                    Console.WriteLine("SWIPE DOWN");
                    break;
            }
        }
    }
}