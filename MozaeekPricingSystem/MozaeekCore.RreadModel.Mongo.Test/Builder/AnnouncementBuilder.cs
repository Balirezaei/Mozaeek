using System;
using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RreadModel.Mongo.Test.Builder
{
    public class AnnouncementBuilder
    {
        private string _title;
        private string _description;
        private List<PointQuery> _pointQueries;

        public AnnouncementBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public AnnouncementBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public AnnouncementBuilder WithPoints(List<PointQuery> points)
        {
            _pointQueries = points;
            return this;
        }

        public AnnouncementQuery Build()
        {
            return new AnnouncementQuery(1, _title, _pointQueries,
                new RequestTargetBuilder().BuildSampleTargetQueryWithLableAndRequestOrg(), DateTime.Now, Guid.NewGuid());
        }

        public AnnouncementParameter BuildParameter()
        {
            return new AnnouncementParameter()
            {
                EventId = Guid.NewGuid(),
                Title = "test",
                PublishEventDate = DateTime.Now,
                RequestTargetId = new RequestTargetBuilder().BuildSampleTargetQueryWithLableAndRequestOrg().Id
            };
        }
    }
}