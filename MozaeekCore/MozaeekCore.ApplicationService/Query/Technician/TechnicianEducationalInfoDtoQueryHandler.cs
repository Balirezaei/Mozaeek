using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    //public class TechnicianEducationalInfoDtoQueryHandler : IBaseAsyncQueryHandler<FindByKey, TechnicianEducationalInfoDto>
    //{
    //    private readonly ITechnicianRepository repository;

    //    public TechnicianEducationalInfoDtoQueryHandler(ITechnicianRepository repository)
    //    {
    //        this.repository = repository;
    //    }
    //    public async Task<TechnicianEducationalInfoDto> HandleAsync(FindByKey query)
    //    {
    //        var res = await repository.FindWithEducationInfo(query.Id);
    //        return new TechnicianEducationalInfoDto()
    //        {
    //            TechnicianId = res.Id,
    //            EducationFieldId = res.TechnicianEducationalInfo != null ? res.TechnicianEducationalInfo.EducationFieldId : 0,
    //            EducationGradeId = res.TechnicianEducationalInfo != null ? res.TechnicianEducationalInfo.EducationGradeId : 0,

    //        };
    //    }
    //}
}