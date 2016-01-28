using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Onliner.Models;
using Xamarin.Forms;

namespace Onliner.Helpers
{
    public class FeedReader
    {
        private static readonly List<FeedItem> _feedItems = new List<FeedItem>();

        public static async Task<IEnumerable<FeedItem>> ParseFeedsAsync(string feedUrl)
        {


                string src = string.Empty;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(feedUrl);
                WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    src = reader.ReadToEnd();

                }


                var xdoc = XDocument.Parse(src);
                XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");

                _feedItems.Clear();

                foreach (var item in xdoc.Descendants("item"))
                {
                    _feedItems.Add(new FeedItem
                    {
                        Title = (string)item.Element("title"),
                        Description = (string)item.Element("description"),
                        Link = (string)item.Element("link"),
                        PublishDate = (string)item.Element("pubDate"),
                        Category = (string)item.Element("category"),
                        Image = item.Element(media + "thumbnail") != null ? item.Element(media + "thumbnail").Attribute("url").Value : ""
                    });

                }


                return _feedItems;

            }
            
        
    }
}
