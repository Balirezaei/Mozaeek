using System;
using System.Linq;

namespace MozaeekCore.RSSRetrive.Service
{
    public interface IRssRetriveFacade
    {
        void ReadAndFetchData();
    }
    public class RssRetriveFacade : IRssRetriveFacade
    {
        private readonly IRSSManager _rssManager;
        private readonly ILogger _logger;

        public RssRetriveFacade(IRSSManager rssManager, ILogger logger)
        {
            _rssManager = rssManager;
            _logger = logger;
        }

        public void ReadAndFetchData()
        {
            var rssReadyToRead = _rssManager.GetALLRssReadyToRead();
            foreach (var rss in rssReadyToRead)
            {
                try
                {
                    var newsReadToSave = new ReadRssFeed().GetNews(rss.Url);

                    if (!newsReadToSave.Any())
                    {
                        continue;
                    }

                    var processedNews = _rssManager.ReCheckRssResult(newsReadToSave, rss.Id);

                    if (!processedNews.Any())
                    {
                        continue;
                    }
                    processedNews = processedNews.Select(m =>
                    {
                        m.Source = rss.Source;
                        m.RSSId = rss.Id;
                        m.ModifiedDate = DateTime.Now;
                        m.CreateDate = DateTime.Now;
                        return m;
                    }).ToList();

                    _rssManager.SaveRssNewsCollection(processedNews);
                    _rssManager.UpdateRetriveHistory(rss.Id, processedNews.Count, true);
                    _rssManager.SaveChange();
                }
                catch (Exception e)
                {
                    _rssManager.UpdateRetriveHistory(rss.Id, 0, false);
                    _rssManager.SaveChange();
                    _logger.DoLog(e.Message);
                    if (e.InnerException != null) _logger.DoLog(e.InnerException.Message);
                }
               
            }
        }

    }
}