using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;

namespace Onliner.iOS.Tabbed
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		// Shared GA tracker
		public IGAITracker Tracker;

		public static readonly string TrackingId = "UA-52276602-2";

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{

			//Styles
			UITabBar.Appearance.TintColor = UIColor.Red;


			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// If you have defined a root view controller, set it here:
			window.RootViewController = new TabController ();
			
			// make the window visible
			window.MakeKeyAndVisible ();

			// Optional: set Google Analytics dispatch interval to e.g. 20 seconds.
			GAI.SharedInstance.DispatchInterval = 20;

			// Optional: automatically send uncaught exceptions to Google Analytics.
			GAI.SharedInstance.TrackUncaughtExceptions = true;

			// Initialize tracker.
			Tracker = GAI.SharedInstance.GetTracker (TrackingId);
			
			return true;
		}
			

	}
}

