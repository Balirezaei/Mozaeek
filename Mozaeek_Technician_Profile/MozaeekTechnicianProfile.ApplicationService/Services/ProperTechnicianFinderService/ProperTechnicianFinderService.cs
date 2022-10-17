using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract;
using MozaeekTechnicianProfile.Domain;
using MozaeekTechnicianProfile.UserQuestionProgress;

namespace MozaeekTechnicianProfile.ApplicationService.Services
{
    public interface IProperTechnicianFinderService
    {
        Task<ProperTechnicianForQuestionAnswering> FindProperTechnician(UserQuestionForTechnicianProcess questiondetail);
    }
    public class ProperTechnicianFinderService : IProperTechnicianFinderService
    {
        private readonly ITechnicianRepository _technicianRepository;

        public ProperTechnicianFinderService(ITechnicianRepository technicianRepository)
        {
            _technicianRepository = technicianRepository;
        }

        public async Task<ProperTechnicianForQuestionAnswering> FindProperTechnician(UserQuestionForTechnicianProcess questiondetail)
        {
            await _technicianRepository.GetTechnicianById(0);
            return null;
        }
    }
}