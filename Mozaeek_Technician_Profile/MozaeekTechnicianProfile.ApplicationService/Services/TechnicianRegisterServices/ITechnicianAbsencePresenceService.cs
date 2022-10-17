using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;

namespace MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices
{
    public interface ITechnicianAbsencePresenceService
    {
        Task<TechnicianAbsencePresenceResult> ActiveTechnician(long technicianId);
        Task<TechnicianAbsencePresenceResult> InactiveTechnician(long technicianId);
        Task<TechnicianAbsencePresenceResult> GetCurrentTechnicianState(long id);
    }
}