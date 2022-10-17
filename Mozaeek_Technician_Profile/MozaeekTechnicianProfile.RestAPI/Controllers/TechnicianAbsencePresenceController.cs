using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;
using MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices;

namespace MozaeekTechnicianProfile.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TechnicianAbsencePresenceController : Controller
    {
        private readonly ITechnicianAbsencePresenceService _technicianAbsencePresenceService;
        public TechnicianAbsencePresenceController(ITechnicianAbsencePresenceService technicianAbsencePresenceService)
        {
            _technicianAbsencePresenceService = technicianAbsencePresenceService;
        }
        [HttpPost]
        public Task<TechnicianAbsencePresenceResult> Active(ActiveTechnicianInput input)
        {
            return _technicianAbsencePresenceService.ActiveTechnician(input.TechnicianId);
        }

        [HttpPost]
        public Task<TechnicianAbsencePresenceResult> Inactive(DeactiveTechnicianInput input)
        {
            return _technicianAbsencePresenceService.InactiveTechnician(input.TechnicianId);
        }

        [HttpGet]
        public Task<TechnicianAbsencePresenceResult> CurrentTechnicianState(long id)
        {
            return _technicianAbsencePresenceService.GetCurrentTechnicianState(id);
        }
    }
}