using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface ITechnicianQueryFacade
    {
        Task<TechnicianDto> GetTechnicianById(int id);
        Task<PagedListResult<TechnicianDto>> GetAll();

        Task<InitTechnicianEducationalInfo> GetInitTechnicianEducationalInfo();
        Task<TechnicianEducationalInfoDto> GetTechnicianEducationalInfoByTechnicianId(long id);
        Task<TechnicianContactInfoDto> GetTechnicianContactInfoByTechnicianId(long id);
        Task<TechnicianPersonalInfoDto> GetTechnicianPersonalInfoByTechnicianId(long id);
        
        Task<TechnicianSubjectDto> GetTechnicianSubjectByTechnicianId(long id);
        Task<TechnicianPointDto> GetTechnicianPointByTechnicianId(long id);
        Task<TechnicianRequestDto> GetTechnicianRequestByTechnicianId(long id);
   
    }
}
