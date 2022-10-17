using System.Collections.Generic;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// رشته تحصیلی
    /// </summary>
    public class EducationField : BasicInfo
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public virtual EducationField Parent { get; set; }

        public virtual ICollection<EducationField> SubEducationFields { get; set; }
        public virtual ICollection<TechnicianEducationalInfo> TechnicianEducationalInfos { get; set; }
    }
}