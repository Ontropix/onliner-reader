using System;
using MonoTouch.UIKit;

namespace Onliner.iOS.Tabbed
{
	public class TabController: UITabBarController 
	{
		public TabController ()
		{
			var tech = new UINavigationController () {
				Title = "Технологии",
				ViewControllers = new UIViewController[] {
					new FeedViewController () {
						FeedSource = "http://tech.onliner.by/feed",
						Title = "Технологии",
					}
				}
			};

			tech.TabBarItem.Image = UIImage.FromBundle("tech");

			var auto = new UINavigationController () {
				Title = "Авто",
				ViewControllers = new UIViewController[] {
					new FeedViewController () {
						FeedSource = "http://auto.onliner.by/feed",
						Title = "Авто"
					}
				}
			};

			auto.TabBarItem.Image = UIImage.FromBundle("auto");

			var people = new UINavigationController () {
				Title = "Люди",
				ViewControllers = new UIViewController[] {
					new FeedViewController () {
						FeedSource = "http://people.onliner.by/feed",
						Title = "Люди"
					}
				}
			};

			people.TabBarItem.Image = UIImage.FromBundle("people");

			var realt = new UINavigationController () {
				Title = "Недвижимость",
				ViewControllers = new UIViewController[] {
					new FeedViewController () {
						FeedSource = "http://realt.onliner.by/feed",
						Title = "Недвижимость"
					}
				}
			};

			realt.TabBarItem.Image = UIImage.FromBundle("realt");

			ViewControllers = new UIViewController[] {
				tech,
				auto,
				people,
				realt
			};

		}
	}
}

