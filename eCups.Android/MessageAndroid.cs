using System;
using Android.Media;
using eCups.Droid;
using Android.App;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]

namespace eCups.Droid
{
	public class MessageAndroid : IMessage
	{
		public void LongAlert(string message)
		{
			Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
		}

		public void ShortAlert(string message)
		{
			Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
		}
	}
}