
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

using Xamarin.Forms;


[assembly: Dependency(typeof(Onliner.Android.WindowService))]
namespace Onliner.Android
{

	public class WindowService: Onliner.IWindowService
	{
		public static  Xamarin.Forms.Size size;

		public Xamarin.Forms.Size Bounds {
			get
			{
				return size;
			}
		}
	}
}

