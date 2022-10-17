using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;

namespace MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices
{
    public interface ITechnicianRegisterService
    {
        Task<int> CreateProfile(CreateTechnicianProfileDto input);
        Task<bool> UpdateTechnicianContactInfo(UpdateTechnicinaContactInfoDto input);
     
    }
}
