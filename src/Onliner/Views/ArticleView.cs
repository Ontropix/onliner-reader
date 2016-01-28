using System;
using Onliner.Models;
using Xamarin.Forms;

namespace Onliner
{
    public class ArticleView : ContentPage
    {
        public ArticleViewModel ViewModel
        {
            get
            {
                return BindingContext as ArticleViewModel;
            }
        }

        private WebView webView;

        

        public ArticleView(FeedItem feedItem)
        {
            BindingContext = new ArticleViewModel();

            ViewModel.ArticleSource = feedItem.Link;
            Title = feedItem.Title;

            var activity = new ActivityIndicator
            {
                Color = Color.Gray,
                IsEnabled = true
            };
            activity.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
            activity.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

         
            webView = new WebView();

            IWindowService windowService = DependencyService.Get<IWindowService>();
            Size size = windowService.Bounds;  

            AbsoluteLayout layout = new AbsoluteLayout();
            layout.Children.Add(activity, new Rectangle(0, 0, size.Width, 40));

            if (Device.OS == TargetPlatform.iOS)
            {
                Button comments = new Button()
                {
                    Text = "Комментарии",
                    BackgroundColor = Color.FromRgba(0.5, 0.5, 0.5, 0.8)
                };

                comments.Clicked += (sender, e) =>
                {
                    Navigation.PushAsync(new CommentsView(ViewModel.Article));
                };

				layout.Children.Add(comments, new Rectangle(5, size.Height - 110, size.Width - 10, 40));
				layout.Children.Add(webView, new Rectangle(0, 0, size.Width, size.Height - 110));
            }
            else
            {
				var comments = new ToolbarItem("comments", "comments.png",
                                              () => Navigation.PushModalAsync(new CommentsView(ViewModel.Article)));

				ToolbarItems.Add(comments);

				layout.Children.Add(webView, new Rectangle(0, 0, size.Width, size.Height));
            }

            Content = layout;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadData();

            webView.Scale = 1.0;
            webView.VerticalOptions = LayoutOptions.Start;
            webView.Source = new HtmlWebViewSource()
            {
               
                Html = ViewModel.Article.Body,
            };
        }
    }
}

