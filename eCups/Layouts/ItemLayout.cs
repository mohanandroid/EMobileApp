using System;
using eCups.e.Images;
using eCups.e.Labels;
using eCups.Helpers;
using eCups.Models.Custom;
using Xamarin.Forms;

namespace eCups.Layouts
{
    public class ItemLayout
    {
        public Grid Content;
        public StackLayout Container;

        public ActiveLabel Name;
        public ActiveLabel ShortDescription;
        public ActiveLabel LongDescription;
        public ActiveImage MainImage;


        public ItemLayout(Item item)
        {
            Content = new Grid();

            Container = new StackLayout { Orientation = StackOrientation.Vertical };

            Name = new ActiveLabel(item.Name, Units.FontSizeL, FontName.LatoBold, Color.Transparent, Color.White, null);

            Container.Children.Add(Name.Content);

            if (item.ShortDescription.Length > 0)
            {
                ShortDescription = new ActiveLabel(item.ShortDescription, Units.FontSizeL, FontName.LatoBold, Color.Transparent, Color.White, null);
                Container.Children.Add(ShortDescription.Content);
            }

            if (item.LongDescription.Length > 0)
            {
                LongDescription = new ActiveLabel(item.LongDescription, Units.FontSizeL, FontName.LatoBold, Color.Transparent, Color.White, null);
                Container.Children.Add(LongDescription.Content);
            }

            if (item.MainImage.Length > 0)
            {
                MainImage = new ActiveImage(item.MainImage, Units.HalfScreenWidth, Units.HalfScreenWidth, null, null);
                MainImage.Image.Aspect = Aspect.AspectFill;
                Container.Children.Add(MainImage.Content);
            }
            
            

            Content.Children.Add(Container);

        }
    }
}
