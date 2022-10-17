using System.Collections.Generic;
using MozaeekCore.Common.ExtentionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// اطلاعات هویتی
    /// </summary>
    public class TechnicianPersonalInfo
    {
        public TechnicianPersonalInfo(long id, string firstName, string lastName, string nationalCode, string identityNumber)
        {
            Id = id;
            FirstName = firstName.Recheck();
            LastName = lastName.Recheck();
            NationalCode = nationalCode.Recheck();
            IdentityNumber = identityNumber.Recheck();
        }

        public void Update(string firstName, string lastName, string nationalCode, string identityNumber)
        {
            FirstName = firstName.Recheck();
            LastName = lastName.Recheck();
            NationalCode = nationalCode.Recheck();
            IdentityNumber = identityNumber.Recheck();
        }

        public long Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalCode { get; private set; }

        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        public string IdentityNumber { get; private set; }

        // public virtual TechnicianAttachment PersonalPhoto { get; set; }
        // public long? PersonalPhotoId { get; set; }

        public virtual ICollection<Technician> Technicians { get; set; }

    }
}