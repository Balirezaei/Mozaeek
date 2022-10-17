using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MozaeekCore.Domain.Identity;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = RoleNames.BasiInfo + "," + RoleNames.Admin)]
    public class PropertyController : ControllerBase
    {

    }
}
