using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;
using MozaeekCore.QueryModel;
using System.Linq;
namespace MozaeekCore.Mapper
{
    public static class TechnicianProfile
    {
        public static EducationFieldDto GetEducationFieldDto(this EducationField domain)
        {
            return new EducationFieldDto()
            {
                Id = domain.Id,
                Title = domain.Title
            };
        }

        public static EducationGradeDto GetEducationGradeDto(this EducationGrade domain)
        {
            return new EducationGradeDto()
            {
                Id = domain.Id,
                Title = domain.Title
            };
        }
        public static TechnicianDto GetTechnicianDto(this TechnicianQuery domain)
        {
            return new TechnicianDto()
            {
                Address=domain.Address,
                DefiniteRequestOrgIds=domain.DefiniteRequestOrgs?.Select(s => s.Id)?.ToList(),
                CreateDateTime=domain.CreateDateTime,
                Attachments=domain.Attachments.Select(a=>new TechnicianAttachmentDto() {FileName=a.FileName,Url=a.FileHttpAddress,Id=a.FileId} ).ToList(),
                Email=domain.Email,
                FirstName=domain.FirstName,
                FirstVerification=domain.FirstVerification,
                Id=domain.Id,
                LastName=domain.LastName,
                NationalId=domain.NationalId,
                OfflineRequestTargetIds=domain.OfflineRequestTargets?.Select(s => s.Id)?.ToList(),
                PhoneNumber=domain.PhoneNumber,
                PointId=domain.Point?.Id,
                PostalCode=domain.PostalCode,
                SecondVerification=domain.SecondVerification,
                SubjectIds=domain.Subjects?.Select(s=>s.Id)?.ToList(),
                TechnicianType=domain.TechnicianType
            
            };
        }

    }
}