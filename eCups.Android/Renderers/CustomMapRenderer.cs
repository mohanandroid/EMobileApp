using System;
using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps;
using CustomRenderer.Droid;
using eCups.Components.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
//using Xamarin.Forms.Platform.Android;
using eCups.e.CustomMapPin;
using Android.Gms.Maps.Model;
using Android.Widget;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace CustomRenderer.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        List<eCupPin> customPins;
        public eCups.Pages.Custom.Map page;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            markerOptions.SetTitle(pin.Label);
            markerOptions.SetSnippet(pin.Address);
            markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(eCups.Droid.Resource.Drawable.customPin));
            markerOptions.Anchor(0.8f, 1);
            return markerOptions;
        }

        private void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            eCupPin customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            /*if (!string.IsNullOrWhiteSpace(customPin.URL)) //visit web page on popup click (redundant if using URL Text)
            {
                var url = global::Android.Net.Uri.Parse(customPin.URL);
                var intent = new Intent(Intent.ActionView, url);
                intent.AddFlags(ActivityFlags.NewTask);
                global::Android.App.Application.Context.StartActivity(intent);
            }*/
        }

        public global::Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = global::Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as global::Android.Views.LayoutInflater;
            if (inflater != null)
            {
                global::Android.Views.View view;

                eCupPin customPin = GetCustomPin(marker);
                //page.UpdateInfo(customPin);

                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                if (customPin.Name.Equals("Xamarin"))
                {
                    //view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
                    //view = inflater.Inflate(Resource.Layout.FlyoutContent, null);
                }
                else
                {
                    //view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
                    //view = inflater.Inflate(Resource.Layout.FlyoutContent, null);
                }

                customPin.SetFocusToThis();

                //var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                //var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                /*if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }*/

                return null;
            }
            return null;
        }

        public eCupPin GetCustomPin(Marker marker)
        {
            Position position = new Position(marker.Position.Latitude, marker.Position.Longitude);
            foreach(eCupPin pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        public global::Android.Views.View GetInfoWindow(Marker marker)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
