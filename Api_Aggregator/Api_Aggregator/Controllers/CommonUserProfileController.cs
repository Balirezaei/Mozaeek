using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.AggregationServices;
using Api_Aggregator.Contract.MediationDtos;

namespace Api_Aggregator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonUserProfileController : ControllerBase
    {
        private readonly IUserProfileLabelService _profileLabelService;

        public CommonUserProfileController(IUserProfileLabelService profileLabelService)
        {
            _profileLabelService = profileLabelService;
        }

        [HttpGet]
        public Task<UnionUserProfileCharacteristicSelectAndUnSelectedDto> GetAllUserProfileCharacteristicByOwner(int ownerId)
        {
            return _profileLabelService.GetUnionUserProfileCharacteristicSelectAndUnSelectedDto(ownerId);
        }
    }
}
