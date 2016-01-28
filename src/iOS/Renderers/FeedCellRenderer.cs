using System;
using Xamarin.Forms.Platform.iOS;
using Onliner.Helpers;
using Xamarin.Forms;
using MonoTouch.UIKit;

[assembly: ExportCell(typeof(ViewCell), typeof(Onliner.iOS.FeedCellRenderer))]

namespace Onliner.iOS
{

	public class FeedCellRenderer: ViewCellRenderer
	{
		public override MonoTouch.UIKit.UITableViewCell GetCell (Cell item, MonoTouch.UIKit.UITableView tv)
		{
			var cellView = base.GetCell (item, tv);
			var scrollView = cellView.Subviews [0];
			UIView contentView = scrollView.Subviews [0];
			//contentView.BackgroundColor = Color.Red.ToUIColor ();
			return cellView;
		}
	}
}

