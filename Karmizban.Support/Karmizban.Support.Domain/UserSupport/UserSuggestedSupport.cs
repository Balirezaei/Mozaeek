using System;
using System.Collections;
using System.Collections.Generic;
using Karmizban.Support.Common;

namespace Karmizban.Support.Domain
{
    public class UserSuggestedSupport
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreateDate { get; private set; }
        public virtual ICollection<UserRequestSupport> UserRequestSupports { get; set; }

        public UserSuggestedSupport(string title, string description)
        {
            Title = title.Recheck();
            Description = description.Recheck();
            CreateDate = DateTime.Now;
        }
        protected UserSuggestedSupport() { }
    }
}
