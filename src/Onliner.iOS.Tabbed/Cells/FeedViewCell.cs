using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Onliner.Models;

namespace Onliner.iOS.Tabbed
{
	public partial class FeedViewCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("FeedViewCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("FeedViewCell");

		public FeedViewCell (IntPtr handle) : base (handle)
		{
		}

		public static FeedViewCell Create ()
		{
			return (FeedViewCell)Nib.Instantiate (null, null) [0];
		}

		public void Update (FeedItem item)
		{
			this.Body.Text = item.Caption;
			this.Date.Text = item.PublishDate;
		}
			
	}
}

