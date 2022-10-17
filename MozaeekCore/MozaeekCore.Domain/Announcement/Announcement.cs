using System;
using System.Collections;
using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Domain;
using MozaeekCore.Exceptions;

namespace MozaeekCore.Domain
{
    public class AnnouncementRequestOrg
    {
        public long Id { get; set; }

        public long AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }

        public long RequestOrgId { get; set; }
        public virtual RequestOrg RequestOrg { get; set; }
    }
    public class AnnouncementLabel
    {
        public long Id { get; set; }
        public long AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }

        public long LabelId { get; set; }
        public virtual Label Label { get; set; }

    }
    public class AnnouncementSubject
    {
        public long Id { get; set; }
        public long AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }

        public long SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
    public class Announcement : AggregateRootBase
    {
        protected Announcement()
        {
        }
        public long Id { get; private set; }
        public string Title { get; private set; }

        public DateTime ReleaseDate { get; private set; }
        public string Description { get; private set; }
        public string Summary { get; private set; }
        public long? FileId { get; private set; }
        public virtual ICollection<AnnouncementSubject> AnnouncementSubjects { get; private set; }
        public virtual ICollection<AnnouncementLabel> AnnouncementLabels { get; private set; }
        public virtual ICollection<AnnouncementRequestOrg> AnnouncementRequestOrgs { get; private set; }

        public virtual ICollection<AnnouncementPoint> AnnouncementPoints { get; private set; }
        public virtual MosaikFile? File { get; private set; }

        public long? RequestId { get; private set; }
        public virtual Request Request { get; set; }
        public bool HasRequest { get; private set; }

        public Announcement(long id, string title, string description, string summary, List<AnnouncementPoint> announcementPoints, bool hasRequest, long? fileId, ICollection<AnnouncementSubject> announcementSubjects, ICollection<AnnouncementLabel> announcementLabels, ICollection<AnnouncementRequestOrg> announcementRequestOrgs)
        {
            Id = id;
            Title = title.Recheck();
            ReleaseDate = DateTime.Now;
            Description = description.Recheck();
            Summary = summary.Recheck();
            AnnouncementPoints = announcementPoints;
            FileId = fileId;
            AnnouncementSubjects = announcementSubjects;
            AnnouncementLabels = announcementLabels;
            AnnouncementRequestOrgs = announcementRequestOrgs;
            this.HasRequest = hasRequest;
        }

        public void UpdateAssociations(List<AnnouncementPoint> announcementPoints)
        {
            this.AnnouncementPoints = announcementPoints;
        }

        public void Update(string title, string description, string summary, ICollection<AnnouncementSubject> announcementSubjects, ICollection<AnnouncementLabel> announcementLabels, ICollection<AnnouncementRequestOrg> announcementRequestOrgs, bool hasRequest, long? fileId = null)
        {
            Title = title.Recheck();
            Description = description.Recheck();
            Summary = summary.Recheck();
            if (fileId != null && fileId != 0)
            {
                FileId = fileId;
                File = null;
            }
            this.HasRequest = hasRequest;
            AnnouncementSubjects = announcementSubjects;
            AnnouncementLabels = announcementLabels;
            AnnouncementRequestOrgs = announcementRequestOrgs;
        }

        // public void SetPhoto(byte[] source, string fileName, string fileExtension)
        // {
        //     FileExtension = fileExtension;
        //     Source = source;
        //     FileName = fileName;
        // }
        public void AssignRequest(Request request)
        {
            //if (request.RequestTargetId != this.RequestTargetId)
            //{
            //    throw new UserFriendlyException("امکان اتصال این کارخواست وجود ندارد");
            //}
            if (this.RequestId != 0)
            {
                throw new UserFriendlyException("این اعلان قبلا به کارخواست متصل شده است.");
            }
            if (request.Id != 0)
            {
                this.RequestId = request.Id;
            }
            else
            {
                this.Request = request;
            }
        }
    }
}