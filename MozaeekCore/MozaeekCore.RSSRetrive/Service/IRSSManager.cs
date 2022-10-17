using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.RSSRetrive.Context;
using MozaeekCore.RSSRetrive.Model;
using MozaeekCore.ViewModel;

namespace MozaeekCore.RSSRetrive.Service
{
    public interface IRSSManager
    {
        List<RssDto> GetALLRssReadyToRead();

        List<RSSNews> ReCheckRssResult(List<RSSNews> input, long rssId);
        void SaveRssNewsCollection(List<RSSNews> input);
        void SaveChange();
        void UpdateRetriveHistory(long rssId, int newsCount, bool isSuccess);
    }

    public class RSSManager : IRSSManager
    {
        private readonly FeedContext _context;

        public RSSManager(FeedContext context)
        {
            _context = context;
        }

        public List<RssDto> GetALLRssReadyToRead()
        {
            //var result = new List<RssDto>();
            //result.Add(new RssDto() { Id = 2, Source = "تجارت نیوز", Url = "https://tejaratnews.com/feed" });
            //return result;

            List<RssDto> res = _context.RssDtos.FromSqlRaw(@"SELECT TOP (10) r.[Id]
                                                                 ,r.[Url]
                                                                 ,r.[Source]
                                                                 ,dateadd(HOUR, r.IntervalDataReceiveHours,h.ModifiedDate )
                                                                 FROM [dbo].[RSS] r 
                                                                 LEFT OUTER JOIN (    
                                                                 SELECT MAX(ModifiedDate) ModifiedDate ,RssId
                                                                 FROM [dbo].RssRetriveHistories
																 WHERE IsSuccess = 1
                                                                 GROUP BY RssId
                                                                 ) as h ON  r.Id = h.RssId
                                                                 WHERE h.ModifiedDate IS NULL OR dateadd(HOUR, r.IntervalDataReceiveHours,h.ModifiedDate ) < getdate() AND IsActive = 1
                                                             ").ToList();



            return res;
        }


        public List<RSSNews> ReCheckRssResult(List<RSSNews> input,long rssId)
        {
            var beginningDate = input
                .Min(m => m.CreateDate).AddDays(-7);

            var preSaveNews = _context.RssNewses
                .Where(m => m.RSSId == rssId && m.CreateDate >= beginningDate)
                .Select(m => m.Link)
                .ToList();

            return input.Where(m => preSaveNews.All(z => z != m.Link)).ToList();
        }

        public void SaveRssNewsCollection(List<RSSNews> input)
        {
            _context.RssNewses.AddRange(input);
        }

        public void UpdateRetriveHistory(long rssId, int newsCount,bool isSuccess)
        {
            _context.RssRetriveHistories.Add(new RssRetriveHistory()
            {
                ModifiedDate = DateTime.Now,
                NewsCount = newsCount,
                RssId = rssId,
                IsSuccess = isSuccess
            });
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}