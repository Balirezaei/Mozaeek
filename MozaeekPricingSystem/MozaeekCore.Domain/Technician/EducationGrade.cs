using System.Collections.Generic;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// رده تحصیلی ،لیسانس
    /// </summary>
    public class EducationGrade : BasicInfo
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<TechnicianEducationalInfo> TechnicianEducationalInfos { get; set; }
    }
}