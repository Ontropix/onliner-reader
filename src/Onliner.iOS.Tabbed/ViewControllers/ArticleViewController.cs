using System;
using MonoTouch.UIKit;
using System.Drawing;
using Onliner.Models;
using GoogleAnalytics.iOS;

namespace Onliner.iOS.Tabbed
{
	public class ArticleViewController: UIViewController
	{
		ArticleViewModel ViewModel;
		UIActivityIndicatorView indicator;
		FeedItem feedItem;
		UIWebView webView;

		public ArticleViewController (FeedItem feedItem)
		{
			this.feedItem = feedItem;
			ViewModel = new ArticleViewModel ();
			Title = "Статья";
			EdgesForExtendedLayout = UIRectEdge.None;
		}

		public async override void ViewDidLoad ()
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

			UIBarButtonItem comments = new UIBarButtonItem ("Комментарии",
				                           UIBarButtonItemStyle.Bordered,
				                           (s, e) => NavigationController.PushViewController (new CommentsViewController (ViewModel.Article), true)
			                           );

			comments.TintColor = UIColor.Red;	

			indicator = new UIActivityIndicatorView (
				new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height)
			);
			indicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
			indicator.AutoresizingMask =  UIViewAutoresizing.FlexibleMargins;
			indicator.Opaque = false;
			indicator.AutosizesSubviews = true;
			this.View.AddSubview (indicator);

			ViewModel.ArticleSource = feedItem.Link;

			indicator.StartAnimating ();
			webView.Hidden = true;

			try {

				await ViewModel.LoadData ();
			
			} catch (System.Net.WebException) {

				indicator.StopAnimating ();
				webView.Hidden = false;

				UIAlertView alert = new UIAlertView ("Ошибка", "Проверьте соединение с интерентом", null, "Ок", null);
				alert.Show ();

				return;


			} catch (Exception ex) {

				indicator.StopAnimating ();
				webView.Hidden = false;

				UIAlertView alert = new UIAlertView ("Ошибка", ex.Message, null, "Ок", null);
				alert.Show ();

				return;

			}

			this.NavigationItem.SetRightBarButtonItem (comments, true);
			webView.Hidden = false;

			indicator.StopAnimating ();

			webView.ScalesPageToFit = true;
			webView.LoadHtmlString (ViewModel.Article.AsHtml (), null);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// This screen name value will remain set on the tracker and sent with
			// hits until it is set to a new value or to null.
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Article View");

			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			webView.Frame = new RectangleF (0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height);
		    indicator.Frame = new RectangleF (0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height);
		}
	}
}

