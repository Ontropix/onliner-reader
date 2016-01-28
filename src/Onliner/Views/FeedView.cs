using System;
using Onliner.Cells;
using Onliner.Helpers;
using Onliner.Models;
using Xamarin.Forms;

namespace Onliner
{
	public class FeedView : ContentPage
	{
		public FeedViewModel ViewModel {
			get { return BindingContext as FeedViewModel; }
		}

		public FeedView ()
		{
			BindingContext = new FeedViewModel ();
            
			var refresh = new ToolbarItem ("refresh", "refresh.png", async () => await ViewModel.LoadItems ());
			ToolbarItems.Add (refresh);

			var stack = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (0, 8, 0, 8)
			};

			var activity = new ActivityIndicator {
				Color = Color.Gray,
				IsEnabled = true
			};
			activity.SetBinding (ActivityIndicator.IsVisibleProperty, "IsBusy");
			activity.SetBinding (ActivityIndicator.IsRunningProperty, "IsBusy");

			stack.Children.Add (activity);

			var listView = new ListView { ItemsSource = ViewModel.FeedItems };
			var cell = new DataTemplate (() => new FeedCell ());
			listView.RowHeight = 50;

			listView.ItemTapped += (sender, args) => {

				if (listView.SelectedItem == null)
					return;

				if (Device.OS == TargetPlatform.WinPhone) {
					Navigation.PushModalAsync (new ArticleView (listView.SelectedItem as FeedItem));
				} else {
					Navigation.PushAsync (new ArticleView (listView.SelectedItem as FeedItem));
				}


				listView.SelectedItem = null;
			};

			listView.ItemTemplate = cell;

			stack.Children.Add (listView);

			Content = stack;
		}

        
		protected async override void OnAppearing ()
		{
			TabbedPage tabbedPage = (TabbedPage)Parent;
			tabbedPage.Title = this.Title;

			base.OnAppearing ();
			if (ViewModel == null || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
				return;

			await ViewModel.LoadItems ();

		}

	}
}

