using System.Collections.Generic;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// اطلاعات تحصیلی
    /// </summary>
    public class TechnicianEducationalInfo
    {
        
        public TechnicianEducationalInfo(long educationGradeId, long educationFieldId)
        {
            EducationGradeId = educationGradeId;
            EducationFieldId = educationFieldId;
        }

        public long Id { get; private set; }
        public long EducationGradeId { get;private set; }
        public EducationGrade EducationGrade { get;private set; }

        public long EducationFieldId { get; private set; }
        public virtual EducationField EducationField { get; private set; }

        public virtual ICollection<Technician> Technicians { get; private set; }

     
    }
}