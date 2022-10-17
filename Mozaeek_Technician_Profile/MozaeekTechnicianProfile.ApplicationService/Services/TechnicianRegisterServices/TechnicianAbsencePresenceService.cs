using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;
using MozaeekTechnicianProfile.Common;
using MozaeekTechnicianProfile.Core.Core;
using MozaeekTechnicianProfile.Domain;

namespace MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices
{
    public class TechnicianAbsencePresenceService : ITechnicianAbsencePresenceService
    {
        private readonly ITechnicianRepository _technicianRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TechnicianAbsencePresenceService(ITechnicianRepository technicianRepository, IUnitOfWork unitOfWork)
        {
            _technicianRepository = technicianRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TechnicianAbsencePresenceResult> ActiveTechnician(long technicianId)
        {
            var technician = await _technicianRepository.GetTechnicianById(technicianId);
            technician.ChangeTechnicianAbsencePresenceState(TechnicianAbsencePresenceState.ActiveReady);
            await _unitOfWork.CommitAsync();
            return new TechnicianAbsencePresenceResult()
            {
                State = technician.LastAbsencePresenceState
            };
        }

        public async Task<TechnicianAbsencePresenceResult> InactiveTechnician(long technicianId)
        {
            var technician = await _technicianRepository.GetTechnicianById(technicianId);
            technician.ChangeTechnicianAbsencePresenceState(TechnicianAbsencePresenceState.Inactive);
            await _unitOfWork.CommitAsync();
            return new TechnicianAbsencePresenceResult()
            {
                State = technician.LastAbsencePresenceState
            };
        }

        public async Task<TechnicianAbsencePresenceResult> GetCurrentTechnicianState(long id)
        {
            var technician = await _technicianRepository.GetTechnicianById(id);
            return new TechnicianAbsencePresenceResult()
            {
                State = technician.LastAbsencePresenceState
            };
        }
    }
}