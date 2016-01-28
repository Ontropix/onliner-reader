using System;
using Onliner.Cells;
using Onliner.Helpers;
using Onliner.Models;
using Xamarin.Forms;

namespace Onliner
{
    public class CommentsView : ContentPage
    {
        public CommentsViewModel ViewModel
        {
            get
            {
                return BindingContext as CommentsViewModel;
            }
        }

        public CommentsView(Article article)
        {
            BindingContext = new ArticleViewModel();

            var stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 8)
            };

			Title = article.Title;

            var listView = new ListView();
            listView.ItemsSource = article.Comments;

            var cell = new DataTemplate(() => new CommentCell());

            listView.ItemTemplate = cell;

            stack.Children.Add(listView);

            Content = stack;
        }
    }
}

