using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Query;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TechnicianDashboardController : ControllerBase
    {
        private readonly ITechnicianDashboardQueryFacade _technicianDashboardQueryFacade;

        public TechnicianDashboardController(ITechnicianDashboardQueryFacade technicianDashboardQueryFacade)
        {
            _technicianDashboardQueryFacade = technicianDashboardQueryFacade;
        }
        [HttpGet]
        public Task<PagedListResult<UserAnnouncementDto>> GetAnnouncements(TechnicianDashboardContract contract)
        {
            return _technicianDashboardQueryFacade.GetAnnouncementByTechnicianType(contract);
        }
    }
}
