using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Linq;

namespace Onliner
{
    public class ArticleViewModel : BaseViewModel
    {
        public string ArticleSource { get; set; }

        private Article article;
        public Article Article
        {
            get { return article; }
            set
            {
                article = value;
                OnPropertyChanged("Article");
            }
        }

        public ArticleViewModel()
        {
        }

        public async Task LoadData()
        {


			try {

				IsBusy = true;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ArticleSource);
            WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string source = reader.ReadToEnd();
                Article = Parse(source);
            }
			  
			Article.Body = string.Format (
				@"<html>
                    <head>
                       <style type='text/css'>
                         img {{border: 0 none; vertical-align: top; display: block; margin-left:auto; margin-right: auto; style: width: 100%; }}
                         p {{ font-size: 36px; margin-left: 5px; text-align: justify }}
                         h1 {{font-size: 44px; text-align: center; }}
                       </style>
                    </head>
                     <body>
					  <br />
                      <h1>{0}</h1>
					  <br /> 
                      <div><img src='{2}' /></div>
                      <section>
                       {1}
                       </section>
                    </body>
                 </html>", 
				Article.Title,
				Article.Body,
				Article.Image
			);

			} finally {
				IsBusy = false;
			}
        }

               private static Article Parse(string souce)
        {

            Article result = new Article();

            Match match = Regex.Match(souce, @"<article.*news_for_copy.*?>(.+?)<\/article>", RegexOptions.Singleline);
            if (match.Success)
            {
                string text = match.Groups[1].Value;

                //Body
                match = Regex.Match(text, @"<div.*b-posts-1-item__text.+?>(.+?)</div>", RegexOptions.Singleline);
                if (match.Success)
                {
                    result.Body = match.Groups[1].Value;
                }

                //Image
                match = Regex.Match(text, @"<figure.+>.*<img.+src=""(.+?)"".*/>", RegexOptions.Singleline);
                if (match.Success)
                {
                    result.Image = match.Groups[1].Value;
                }

                //Title
                match = Regex.Match(text, @"b-posts-1-item__title.+?<span>(.+?)<\/span>", RegexOptions.Singleline);
                if (match.Success)
                {
                    result.Title = match.Groups[1].Value;
                }

                //Date
                match = Regex.Match(text, @"<time.+?datetime=""(.+?)"".*>", RegexOptions.Singleline);
                if (match.Success)
                {
                    string date = match.Groups[1].Value;
                    result.CreatedAt = DateTime.Parse(date);
                }

                //Category
                match = Regex.Match(text, @"b-post-tags-1.+?<strong><a.+?>(.+?)<\/a>", RegexOptions.Singleline);
                if (match.Success)
                {
                    result.Category = match.Groups[1].Value;
                }

                //Tag
                match = Regex.Match(text, @"b-post-tags-1.+?<small><a.+?>(.+?)<\/a>", RegexOptions.Singleline);
                if (match.Success)
                {
                    result.Tag = match.Groups[1].Value;
                }
            };

            result.Comments = ParseComments(souce);

            return result;
        }

        private static ObservableCollection<Comment> ParseComments(string source)
        {
            List<Comment> comments = new List<Comment>();

            MatchCollection matches = Regex.Matches(source, @"<li.+?commentListItem.+?>(.+?)<\/li>", RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                Comment comment = new Comment();

                //Text
                Match subMatch = Regex.Match(match.Value, @"<div.+?comment-content.+?>(.+?)<div class=""comment-actions""", RegexOptions.Singleline);
                if (subMatch.Success)
                {
                    comment.Text = "<div>" + subMatch.Groups[1].Value.Trim();
                }

                //AuthorAvatar
                subMatch = Regex.Match(match.Value, @"<strong.+?author.+?<img.+?src=""(.+?)"".*?>", RegexOptions.Singleline);
                if (subMatch.Success)
                {
                    comment.AuthorAvatar = subMatch.Groups[1].Value;
                }

                //CreatedAt
                subMatch = Regex.Match(match.Value, @"data-comment-date=""(.+?)""", RegexOptions.Singleline);
                if (subMatch.Success)
                {
                    long seconds = Convert.ToInt64(subMatch.Groups[1].Value);
                    comment.CreatedAt = new DateTime(1970, 1, 1).AddSeconds(seconds);
                }

                //AuthorName
                subMatch = Regex.Match(match.Value, @"<strong.+?author.+?figure>(.+?)<\/a>", RegexOptions.Singleline);
                if (subMatch.Success)
                {
                    comment.AuthorName = subMatch.Groups[1].Value;
                }

                comments.Add(comment);
            }


            return new ObservableCollection<Comment>(comments);
        }
	}


}

