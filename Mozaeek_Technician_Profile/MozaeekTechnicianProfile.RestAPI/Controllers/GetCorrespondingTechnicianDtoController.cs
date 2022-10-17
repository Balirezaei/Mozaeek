using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;
using MozaeekTechnicianProfile.ApplicationService.Contract.SearchDto;
using MozaeekTechnicianProfile.ApplicationService.Services.OtpServices;
using MozaeekTechnicianProfile.RestAPI.Services;
using Newtonsoft.Json;

namespace MozaeekTechnicianProfile.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProperTechnicianController : ControllerBase
    {
        [HttpGet]
        public async Task<long> GetTechnicianCount(GetProperTechnicianDto technicianDto)
        {
            return 4;
        }

        [HttpGet]
        public List<ProperTechnicianResult> GetTechnician(GetProperTechnicianDto technicianDto)
        {
            return new List<ProperTechnicianResult>()
            {
                new ProperTechnicianResult()
                {
                    FullName = "کاردان شماره یک",
                    Id = 1
                },
                new ProperTechnicianResult()
                {
                    FullName = "کاردان شماره دو",
                    Id = 2
                },
                new ProperTechnicianResult()
                {
                    FullName = "کاردان شماره سه",
                    Id = 3
                },
                new ProperTechnicianResult()
                {
                    FullName = "کاردان شماره چهار",
                    Id = 3
                }
            };
        }

    }
}
