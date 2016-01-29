using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Onliner.Helpers;
using Onliner.Models;

namespace Onliner
{
	public class FeedViewModel : BaseViewModel
	{

		private ObservableCollection<FeedItem> feedItems = new ObservableCollection<FeedItem> ();

		/// <summary>
		/// gets or sets the feed items
		/// </summary>
		public ObservableCollection<FeedItem> FeedItems {
			get { return feedItems; }
			set {
				feedItems = value;
				OnPropertyChanged ("FeedItems");
			}
		}

		private string feedSource;

		public string FeedSource {
			get {
				return feedSource;
			}
			set {
				feedSource = value;
				OnPropertyChanged ("FeedSource");

			}
		}

		private FeedItem selectedFeedItem;

		/// <summary>
		/// Gets or sets the selected feed item
		/// </summary>
		public FeedItem SelectedFeedItem {
			get { return selectedFeedItem; }
			set {
				selectedFeedItem = value;
				OnPropertyChanged ("SelectedFeedItem");
			}
		}


		public async Task LoadItems ()
		{
			if (IsBusy)
				return;

			try {
				IsBusy = true;

				var feeds = await FeedReader.ParseFeedsAsync (FeedSource);

				FeedItems.Clear ();

				foreach (var feed in feeds) {
					FeedItems.Add (feed);
				}


			} finally {
				IsBusy = false;
			}
		}

	}
}

