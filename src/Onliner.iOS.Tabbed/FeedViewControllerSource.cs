using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.ObjectModel;
using Onliner.Models;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace Onliner.iOS.Tabbed
{
	public class FeedViewControllerSource : UITableViewSource
	{
		public UIImage PlaceholderImage { get; set; }

		UIImage[] images;

		ObservableCollection<FeedItem> FeedItems;
		UIViewController parentController;

		public FeedViewControllerSource (ObservableCollection<FeedItem> feedItems, UIViewController parentController)
		{
			this.parentController = parentController;
			FeedItems = feedItems;

			images = new UIImage[feedItems.Count];

			System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (sender, e) => {
				//No crashes on image failed to load
				e.SetObserved();
			};
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return FeedItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (FeedViewCell.Key) as FeedViewCell;
			if (cell == null)
				cell = FeedViewCell.Create ();

			FeedItem item = FeedItems [indexPath.Item];
			cell.Update (item);
			cell.Tag = indexPath.Item;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			UIImage image = images [indexPath.Item];
			if (image == null) {
				cell.Thumb.Image = PlaceholderImage;
				BeginDownloadingImage (item.Image, indexPath.Item);
			} else {
				cell.Thumb.Image = image;
			}
				
			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			FeedItem comment = FeedItems [indexPath.Item];
			NSString caption = new NSString (comment.Caption);

			RectangleF rect = caption.GetBoundingRect (new SizeF (150, 0), NSStringDrawingOptions.UsesLineFragmentOrigin,
				                  new UIStringAttributes () { }, null);

			return Math.Max (55, rect.Height);
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			FeedItem item = FeedItems [indexPath.Item];
			parentController.NavigationController.PushViewController (new ArticleViewController (item), false);
		}

		async void BeginDownloadingImage (string uri, int index)
		{

			byte[] data = await GetImageData (uri);

			if (data == null) {
				return;
			}

			images [index] = UIImage.LoadFromData (NSData.FromArray (data));

			InvokeOnMainThread (() => {
				var cell = ((UITableViewController)this.parentController).TableView.VisibleCells.Where (c => c.Tag == index).FirstOrDefault ();
				if (cell != null)
					((FeedViewCell)cell).Thumb.Image = images [index];
			});
		}

		async Task<byte[]> GetImageData (string uri)
		{
			byte[] data = null;
			try {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				using (var c = new WebClient ())
					data = await c.DownloadDataTaskAsync (uri);
			}
				catch(System.Exception){
				   //Ignore 
				}
			 finally {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}

			return data;
		}
	}
}

