using System;
using System.Collections;
using System.Collections.Generic;
using MozaeekCore.Core.Domain;

namespace MozaeekCore.Domain
{
    public class Announcement : AggregateRootBase
    {
        protected Announcement() { }
        public long Id { get; private set; }
        public string Title { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public string Description { get; private set; }

        public long RequestTargetId { get; private set; }
        public virtual RequestTarget RequestTarget { get; set; }
        public virtual ICollection<AnnouncementPoint> AnnouncementPoints { get; private set; }
        public Announcement(long id, string title, string description, long requestTargetId, List<AnnouncementPoint> announcementPoints)
        {
            Id = id;
            Title = title;
            ReleaseDate = DateTime.Now;
            Description = description;
            RequestTargetId = requestTargetId;
            AnnouncementPoints = announcementPoints;
        }

        public void UpdateAssociations(List<AnnouncementPoint> announcementPoints)
        {
            this.AnnouncementPoints = announcementPoints;
        }
    }
}