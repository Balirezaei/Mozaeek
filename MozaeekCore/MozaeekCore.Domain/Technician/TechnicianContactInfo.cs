using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// اطلاعات تماس
    /// </summary>
    public class TechnicianContactInfo
    {
        public long Id { get; set; }
        public string MobileNumber { get; private set; }
        public string PhoneNumber { get; private set; }
        public string OfficeNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string Address { get; private set; }

         
        public TechnicianContactInfo(string mobileNumber, string phoneNumber, string officeNumber, string postalCode, string address)
        {
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
            OfficeNumber = officeNumber;
            PostalCode = postalCode;
            Address = address.Recheck();
        }

        public ICollection<Technician> Technicians { get; set; }
    }
}