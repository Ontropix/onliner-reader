using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Onliner
{
	public class Article
	{
		public string Title { get; set; }

		public string Category { get; set; }

		public string Tag { get; set; }

		public DateTime CreatedAt { get; set; }

		public string Body { get; set; }

		public string Image { get; set; }

		public ObservableCollection<Comment> Comments { get; set; }

		public string AsHtml() {
			return Body;
		}

		public string CommentsAsHtml ()
		{
			if (Comments == null || Comments.Count == 0) {
				return null;
			}

			var sb = new System.Text.StringBuilder (
				@"<html>
                    <head>
                       <style type='text/css'>
                         
                         h1 {font-size: 44px; text-align: center; }
                         .or-author {height: 100px;}
                         .or-author img {border: 0 none; width: 100px; margin-left: 5px; max-height: 100px; margin-top: 5px;float:left; margin-right:10px;}
                         .or-author div {                                            
                                     line-height:100px;
                                     -webkit-transform: translateY(-50%)								 
                                     text-align: left; 
                                     font-weight: bold; font-size: 32px; 
                                     }
                         .or-text { font-size: 32px; margin-left: 5px; }
                         .or-date {text-align: right; margin-right: 5px; margin-bottom: 5px; font-size: 24px;}
                         blockquote {border: 1px solid gray; padding: 5px;}
                         cite {font-weight: bold; display: block; }
                         
                       </style>
                    </head>
                    <body>");

			sb.AppendFormat ("<br /><h1>{0}</h1> <br />", this.Title ?? string.Empty);

			foreach (Comment c in Comments) {
				sb.AppendFormat (
					@"<div class='comment'> 
                         <div class='or-author'><img src='{0}' /> <div>{1}</div> </div>
                         <div class='or-text'>{2}</div>
                         <div class='or-date'>{3}</div>
                     </div> 
                     <hr /> ", 
					c.AuthorAvatar, c.AuthorName, 
					c.Text, c.CreatedAt.ToString()
				);
			}

			sb.Append ("</body></html>");

			return sb.ToString ();
		}

	}
	
}
