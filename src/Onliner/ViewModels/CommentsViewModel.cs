using System;

namespace Onliner
{
	public class CommentsViewModel: BaseViewModel
	{
		public CommentsViewModel ()
		{
		}


		private Article article;
		public Article Article { 
			get { return article; } 
			set {
				article = value;
				OnPropertyChanged ("Article");
			}
		}
	}
}

