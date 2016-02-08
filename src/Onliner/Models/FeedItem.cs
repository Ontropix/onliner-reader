using System;
using System.Text.RegularExpressions;

namespace Onliner.Models
{
	public class FeedItem
	{
		public string Description { get; set; }

		public string Link { get; set; }

		private string publishDate;

		public string PublishDate {
			get { return publishDate; }
			set {
				DateTime time;
				if (DateTime.TryParse (value, out time))
					publishDate = time.ToLocalTime ().ToString ();
				else
					publishDate = value;
			}
		}

		public string Author { get; set; }

		public string AuthorEmail { get; set; }

		public string CommentCount { get; set; }

		public string Category { get; set; }

		private string title;

		public string Title {
			get {
				return title;
			}
			set {
				title = value;

			}
		}

		private string caption;

		public string Caption {
			get {
				if (!string.IsNullOrWhiteSpace (caption))
					return caption;

				//get rid of HTML tags
				caption = Regex.Replace (Title, "<[^>]*>", string.Empty);
				caption = caption.Replace ("&nbsp;", " ");

				//get rid of multiple blank lines
				caption = Regex.Replace (caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

				return caption;
			}
		}

		private bool showImage = true;

		public bool ShowImage {
			get { return showImage; }
			set { showImage = value; }
		}

		private string image;

		/// <summary>
		/// When we set the image, mark show image as true
		/// </summary>
		public string Image {
			get { return image; }
			set {
				image = value;
				showImage = true;
			}

		}

	}

}
