using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace eCups.e.CustomMapPin
{
    public class eCupPin : Pin
    {
        public ImageSource Image { get; set; }
        //public Position position;
        public string Name;
        public string URL;
        public string Tel;
        public eCups.Pages.Custom.Map page;

        public eCupPin()
        {
            Image = ImageSource.FromResource("customPin.png");
        }

        public void SetFocusToThis()
        {
            page.UpdateInfo(this);
        }
    }
}
