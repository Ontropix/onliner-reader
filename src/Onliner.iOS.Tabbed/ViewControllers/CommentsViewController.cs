
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;

namespace Onliner.iOS.Tabbed
{
	public class CommentsViewController : UIViewController
	{
		Article article;
		UIWebView webView;

		public CommentsViewController (Article article)
		{
			this.article = article;
			Title = "Комментарии";
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.NavigationBar.SetTitleTextAttributes (
				new UITextAttributes () { TextColor = UIColor.Red}
			);

			this.NavigationController.NavigationBar.TintColor = UIColor.Red;

			this.View.BackgroundColor = UIColor.White;
			webView = new UIWebView (new RectangleF (0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height));
			webView.Delegate = new WebViewDelegate ();

			webView.AutosizesSubviews = true;
			webView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

			this.View.Add (webView);

			webView.ScalesPageToFit = true;
			string html = article.CommentsAsHtml ();
			webView.LoadHtmlString (html, null);

		}

		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			webView.Frame = new RectangleF (0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// This screen name value will remain set on the tracker and sent with
			// hits until it is set to a new value or to null.
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Comments View");

			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
	}

}

