using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    //public class GetTechnicianPersonalInfoDtoQueryHandler : IBaseAsyncQueryHandler<FindByKey, TechnicianPersonalInfoDto>
    //{
    //    private readonly ITechnicianRepository repository;

    //    public GetTechnicianPersonalInfoDtoQueryHandler(ITechnicianRepository repository)
    //    {
    //        this.repository = repository;
    //    }
    //    public async Task<TechnicianPersonalInfoDto> HandleAsync(FindByKey query)
    //    {
    //        var res = await repository.FindWithPersonalInfo(query.Id);
    //        var personalInfo = new TechnicianPersonalInfoDto()
    //        {
    //            TechnicianId = res.Id
    //        };

    //        if (res.TechnicianPersonalInfo != null)
    //        {
    //            personalInfo.FirstName = res.TechnicianPersonalInfo.FirstName;
    //            personalInfo.LastName = res.TechnicianPersonalInfo.LastName;
    //            personalInfo.IdentityNumber = res.TechnicianPersonalInfo.IdentityNumber;
    //            personalInfo.NationalCode = res.TechnicianPersonalInfo.NationalCode;
    //        }

    //        return personalInfo;
    //    }
    //}
}