using System;
using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RreadModel.Mongo.Test.Builder
{
    public class RequestTargetBuilder
    {
        private string _title;
        public RequestTargetBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }
        public RequestTargetQuery BuildSampleTargetQueryWithLableAndRequestOrg()
        {
            return new RequestTargetQuery(1,
                _title,"",
                new List<LabelQuery>() { new LabelBuilder().BuildFirstLevel() }
                , null
                , DateTime.Now,
                Guid.NewGuid(),true
            );
        }

        //public RequestTargetParameter BuildSampleParameter()
        //{
        //    return new RequestTargetParameter(1, _title,
        //        null,
        //        new List<long>() {new LabelBuilder().BuildFirstLevel().Id},
        //        new List<long>() {new RequestOrgBuilder().BuildFirstLevel().Id},
        //        true,
        //        Guid.NewGuid(),
        //        DateTime.Now);
        //}

    }
}