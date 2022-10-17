using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.AggregationServices;
using Api_Aggregator.Contract.MediationDtos;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Api_Aggregator.Infrastructure.ResponseMessages;

namespace Api_Aggregator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonCoreController : ControllerBase
    {
        private readonly IUserProfileLabelService _profileLabelService;
        private readonly IUserProfileSubjectPriceService _userProfileSubjectPriceService;

        public CommonCoreController(IUserProfileLabelService profileLabelService, IUserProfileSubjectPriceService userProfileSubjectPriceService)
        {
            _profileLabelService = profileLabelService;
            _userProfileSubjectPriceService = userProfileSubjectPriceService;
        }

        [HttpPost]
        public Task<PagedListResult<LabelGrid>> GetAllChildrenLabel(GetAllChildrenLabelInput input)
        {
            return _profileLabelService.GetAllLabelChildren(input);
        }

        [HttpGet]
        public Task<SubjectWithPriceDetailDto> GetSubjectPriceDto(long subjectId)
        {
            return _userProfileSubjectPriceService.GetSubjectPriceDto(subjectId);
        }

    }
}
