using System;
using System.Collections.Generic;
using eCups.e.CustomMapPin;
using Xamarin.Forms.Maps;

namespace eCups.Components.Maps
{
    public class CustomMap : Map
    {
        public List<eCupPin> CustomPins { get; set; }
    }
}