using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Common
{
    public class ParentChildHierarchyDto<T>
    {

        public T[] HierarchyResult;
        public long Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
        public long? ParentId { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
    }
}
