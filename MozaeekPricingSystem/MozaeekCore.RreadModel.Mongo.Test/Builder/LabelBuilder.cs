using System;
using System.Collections.Generic;
using System.Text;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RreadModel.Mongo.Test.Builder
{
    public class LabelBuilder
    {
        private string _title;
        private long? _parentId;
        public LabelBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public LabelBuilder WithParentId(long? id)
        {
            _parentId = id;
            return this;
        }

        public LabelQuery BuildFirstLevel()
        {
            return new LabelQuery(1, "کسب و کار", null, DateTime.Now, Guid.NewGuid());
        }
        public LabelQuery BuildSecondLevel()
        {
            return new LabelQuery(2, "ساخت و ساز", 1, DateTime.Now, Guid.NewGuid());
        }

        public LabelQuery BuildThirdLevel()
        {
            return new LabelQuery(3, "مسکونی", 2, DateTime.Now, Guid.NewGuid());
        }

        public LabelQuery Build()
        {
            return new LabelQuery(1, _title, _parentId, DateTime.Now, Guid.NewGuid());

        }
    }
}
