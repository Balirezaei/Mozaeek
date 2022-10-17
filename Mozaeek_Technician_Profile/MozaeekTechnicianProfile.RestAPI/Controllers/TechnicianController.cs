using Microsoft.AspNetCore.Mvc;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;
using MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TechnicianController : Controller
    {
        private readonly ITechnicianRegisterService _technicianRegisterService;
        public TechnicianController(ITechnicianRegisterService technicianRegisterService)
        {
            _technicianRegisterService = technicianRegisterService;
        }
        [HttpPost]
        public async Task<int> CreateProflie(CreateTechnicianProfileDto input)
        {
            return await _technicianRegisterService.CreateProfile(input);
        }
        [HttpPost]
        public async Task<bool> UpdateTechnicainContactInfo(UpdateTechnicinaContactInfoDto input)
        {
            return await _technicianRegisterService.UpdateTechnicianContactInfo(input);
        }
    }
}
