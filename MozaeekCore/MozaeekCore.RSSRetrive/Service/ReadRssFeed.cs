using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using MozaeekCore.RSSRetrive.Model;
using MozaeekCore.ViewModel;

namespace MozaeekCore.RSSRetrive.Service
{
    public class ReadRssFeed
    {

        public List<RSSNews> GetNews(string feedAddress)
        {
            var model = new List<RSSNews>();
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                string downloadString = client.DownloadString(feedAddress);
                XDocument documenssst = XDocument.Parse(downloadString);
                model.AddRange(DecompileRss(documenssst));
            }
            catch (Exception exception)
            {
                try
                {
                    var feedXml = XDocument.Load(feedAddress);
                    model.AddRange(DecompileRss(feedXml));
                }
                catch (Exception e)
                {
                    try
                    {
                        using (var reader = XmlReader.Create(feedAddress))
                        {
                            var rssData = SyndicationFeed.Load(reader);
                            model.AddRange(DecompileRss(rssData));
                        }
                    }
                    catch (Exception exception1)
                    {
                        var feed = new SyndicationFeed("Title", "Description", new Uri(feedAddress), "RSSUrl", DateTime.Now);
                        feed.Copyright = new TextSyndicationContent($"{DateTime.Now.Year} Mitchel Sellers");
                    }


                }
            }
            // model = _service.CheckRssResult(model).ToList();
            return model;
        }

        #region DecompileRss

        private List<RSSNews> DecompileRss(SyndicationFeed feed)
        {
            return (from r in feed.Items
                    where r.PublishDate.DateTime.ToString() != "1/1/0001 12:00:00 AM"
                    select new RSSNews()
                    {
                        Title = r.Title.Text,
                        //  Description = r.Summary.Text,
                        CreateDate = r.PublishDate.DateTime,
                        Link = r.Id != null ? r.Id : (r.Links.Any() ? r.Links[0].Uri.OriginalString : "")
                    }).ToList();
        }

        private List<RSSNews> DecompileRss(XDocument feedXml)
        {
            List<RSSNews> lstNews = new List<RSSNews>();
            foreach (var feed in feedXml.Descendants("item"))
            {
                var newsVm = new RSSNews
                {
                    Title = feed.Element("title").Value,
                    Link = feed.Element("link").Value,
                    Description = feed.Element("description").Value
                };
                var dateString = "";

                try
                {
                    dateString = feed.Element("pubDate").Value.Replace("+0430", "").Replace(" GMT+03:30", "").Replace(" GMT", "").Replace(" +0330", "");
                }
                catch (Exception)
                {
                    dateString = feed.Element("PubDate").Value.Replace(" +0430", "");
                }

                if (dateString.Contains("+"))
                {
                    dateString = (dateString.Split("+")[0]).Trim();
                }

                dateString = dateString.Trim();

                var formatList = new List<string>()
                {
                    "ddd MMM dd yyyy HH:mm:ss",
                    "ddd, d MMM yyyy HH:mm:ss",
                    "ddd, dd MMM yy HH:mm:ss",
                    "ddd, dd MMM yyyy HH:mm:ss",
                    "dd MMM yyyy HH:mm:ss"
                };

                for (int i = 0; i < formatList.Count; i++)
                {
                    try
                    {
                        newsVm.CreateDate = DateTime.ParseExact(dateString, formatList[i], CultureInfo.InvariantCulture, DateTimeStyles.None);
                        break;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                lstNews.Add(newsVm);
            }



            return lstNews;
        }

        #endregion


    }
}