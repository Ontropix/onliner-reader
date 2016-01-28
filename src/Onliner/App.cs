using System;
using Xamarin.Forms;

namespace Onliner
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			var tech = new FeedView () { Title = "Tech" };
			tech.ViewModel.FeedSource = "http://tech.onliner.by/feed";

			var auto = new FeedView () { Title = "Auto" };
			auto.ViewModel.FeedSource = "http://auto.onliner.by/feed";

			var people = new FeedView() { Title = "People"};
			people.ViewModel.FeedSource = "http://people.onliner.by/feed";

			var realt = new FeedView () { Title = "Realt" };
			realt.ViewModel.FeedSource = "http://realt.onliner.by/feed";
            
			var tabbedPage = new TabbedPage () {
				Children = { 
					tech,
					auto,
					people,
					realt				
				}
			};
				
			return new NavigationPage(tabbedPage);
		}
	}
}

