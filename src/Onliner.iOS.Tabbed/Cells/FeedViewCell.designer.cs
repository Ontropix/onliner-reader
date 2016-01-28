// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Onliner.iOS.Tabbed
{
	[Register ("FeedViewCell")]
	partial class FeedViewCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel Body { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Date { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UIImageView Thumb { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Thumb != null) {
				Thumb.Dispose ();
				Thumb = null;
			}

			if (Body != null) {
				Body.Dispose ();
				Body = null;
			}

			if (Date != null) {
				Date.Dispose ();
				Date = null;
			}
		}
	}
}
