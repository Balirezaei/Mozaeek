using MozaeekCore.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MozaeekCore.Enum;
using MozaeekCore.Exceptions;

namespace MozaeekCore.Domain
{
    public class Technician : AggregateRootBase
    {
        protected Technician()
        {

        }

        public Technician(long id, TechnicianType technicianType)
        {
            Id = id;
            TechnicianType = technicianType;
            CreateDateTime = DateTime.Now;
        }


        public long Id { get; set; }
        public DateTime CreateDateTime { get; private set; }
        public TechnicianType TechnicianType { get; set; }

        public long? TechnicianContactInfoId { get; set; }
        public virtual TechnicianContactInfo TechnicianContactInfo { get; set; }

        public long? TechnicianEducationalInformationId { get; set; }
        public virtual TechnicianEducationalInfo TechnicianEducationalInfo { get; set; }

        public long TechnicianPersonalInfoId { get; set; }

        public void AddPersonalInfo(TechnicianPersonalInfo personalInfo)
        {
            this.TechnicianPersonalInfo = personalInfo;
        }

        public void UpdatePersonalInfo(TechnicianPersonalInfo personalInfo)
        {
            this.TechnicianPersonalInfo.Update(personalInfo.FirstName, personalInfo.LastName, personalInfo.NationalCode, personalInfo.IdentityNumber);
        }

        /// <summary>
        /// اطلاعات هویتی
        /// </summary>
        public virtual TechnicianPersonalInfo TechnicianPersonalInfo { get; set; }
        public virtual ICollection<TechnicianAttachment> TechnicianAttachments { get; set; }

        /// <summary>
        /// نقاط فعالیت
        /// </summary>
        public virtual ICollection<TechnicianPoint> TechnicianPoints { get; set; }

        /// <summary>
        /// زمینه های فعالیت
        /// </summary>
        public virtual ICollection<TechnicianSubject> TechnicianSubjects { get; set; }
        /// <summary>
        /// خواست های مورد فعالیت
        /// </summary>
        public virtual ICollection<TechnicianRequest> TechnicianRequests { get; private set; }


        public void AddAttachment(TechnicianAttachment attachment)
        {
            if (this.TechnicianAttachments == null)
            {
                this.TechnicianAttachments = new List<TechnicianAttachment>();
            }
            this.TechnicianAttachments.Add(attachment);
        }

        public void AppendSubjects(IEnumerable<TechnicianSubject> subjects)
        {
            if (this.TechnicianType == TechnicianType.Executive)
            {
                throw new UserFriendlyException("موضوعات به کاردان مجری متصل نمی شوند.");
            }
            this.TechnicianSubjects = subjects.ToList();
        }

        public void AppendRequests(IEnumerable<TechnicianRequest> requests)
        {
            if (this.TechnicianType == TechnicianType.Executive)
            {
                throw new UserFriendlyException("خواست ها به کاردان مشاور متصل نمی شوند.");
            }
            this.TechnicianRequests = requests.ToList();
        }

        public void AppendContactInfo(TechnicianContactInfo contactInfo)
        {
            this.TechnicianContactInfo = contactInfo;
        }

        public void AppendEducationalInfo(TechnicianEducationalInfo info)
        {
            this.TechnicianEducationalInfo = info;
        }

        public void AppendPoints(IEnumerable<TechnicianPoint> points)
        {
            this.TechnicianPoints = points.ToList();
        }
    }
}


