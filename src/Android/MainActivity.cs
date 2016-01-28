using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace Onliner.Android
{
	[Activity (Label = "Onliner", MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

				Xamarin.Forms.Forms.Init (this, bundle);

						SetPage ( App.GetMainPage ());
		
			var disp=	WindowManager.DefaultDisplay;
			var size = new Xamarin.Forms.Size (disp.Height, disp.Width);
			Onliner.Android.WindowService.size = size;

		}
	}
}

