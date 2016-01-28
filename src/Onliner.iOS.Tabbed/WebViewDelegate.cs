
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;

namespace Onliner.iOS.Tabbed
{

	public class WebViewDelegate: UIWebViewDelegate {
		public override bool ShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			if (navigationType == UIWebViewNavigationType.LinkClicked) {
				UIApplication.SharedApplication.OpenUrl (request.Url);
				return false;
			}

			return true;
		}
	}
}
