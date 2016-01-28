using System;
using Xamarin.Forms;
using MonoTouch.UIKit;
using System.Drawing;

[assembly : Dependency(typeof(Onliner.iOS.WindowService))]

namespace Onliner.iOS
{
	public class WindowService: Onliner.IWindowService
	{
		public Xamarin.Forms.Size Bounds {
			get { 
				RectangleF bounds = UIScreen.MainScreen.Bounds;
				return new Xamarin.Forms.Size (bounds.Width, bounds.Height);
			}
		}
	}
}

