using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MonoTouch.UIKit;

[assembly: ExportCell(typeof(ViewCell), typeof(Onliner.iOS.CommentCellRenderer))]

namespace Onliner.iOS
{
	public class CommentCellRenderer: ViewCellRenderer
	{
		public override MonoTouch.UIKit.UITableViewCell GetCell (Cell item, MonoTouch.UIKit.UITableView tv)
		{
			UITableViewCell cell =  base.GetCell (item, tv);
			return cell;
		}
	}
}

