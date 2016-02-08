using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;
using System.Net;

namespace Onliner.iOS.Tabbed
{
	public class FeedViewController : UITableViewController
	{
		public string FeedSource { get; set; }

		FeedViewModel ViewModel;
		UIActivityIndicatorView indicator;

		public FeedViewController () : base (UITableViewStyle.Plain)
		{
			EdgesForExtendedLayout = UIRectEdge.None;
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.White;

			this.NavigationController.NavigationBar.SetTitleTextAttributes (
				new UITextAttributes () { TextColor = UIColor.Red }
			);

			this.NavigationController.NavigationBar.TintColor = UIColor.Red;

			var refresh = new UIBarButtonItem (UIBarButtonSystemItem.Refresh, async (s, e) => await Populate ());

			this.NavigationItem.SetRightBarButtonItem (refresh, false);

			ViewModel = new FeedViewModel ();
			ViewModel.FeedSource = this.FeedSource;

			indicator = new UIActivityIndicatorView (
				new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height)
			);
			indicator.AutosizesSubviews = true;
			indicator.BackgroundColor = UIColor.White;
			indicator.Opaque = false;
			indicator.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			indicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;

			this.View.AddSubview (indicator);

			await Populate ();
		}

		private async System.Threading.Tasks.Task Populate ()
		{

			indicator.StartAnimating ();


			try {
				await ViewModel.LoadItems ();
			} catch (WebException) {

				indicator.StopAnimating ();

				UIAlertView alert = new UIAlertView ("Ошибка", "Проверьте соединение с интернетом", null, "Ok", null);
				alert.Show ();

				return;

			} catch (Exception ex) {

				indicator.StopAnimating ();

				UIAlertView alert = new UIAlertView ("Ошибка", ex.Message, null, "Ok", null);
				alert.Show ();

				return;
			
			}
				
			indicator.StopAnimating ();

			// Register the TableView's data source
			var source = new FeedViewControllerSource (ViewModel.FeedItems, this);
			source.PlaceholderImage = UIImage.FromBundle ("Placeholder.png");
			TableView.Source = source;
			TableView.ReloadData ();
		}

		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			indicator.Frame = new RectangleF (0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// This screen name value will remain set on the tracker and sent with
			// hits until it is set to a new value or to null.
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Feed View");

			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
	}
}

