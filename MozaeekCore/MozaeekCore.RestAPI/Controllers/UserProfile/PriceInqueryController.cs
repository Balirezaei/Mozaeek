using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Facade.Query.UserProfile;

namespace MozaeekCore.RestAPI.Controllers.UserProfile
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PriceInqueryController : ControllerBase
    {
        private readonly IPriceInqueryQueryFacade _priceInqueryQueryFacade;

        public PriceInqueryController(IPriceInqueryQueryFacade priceInqueryQueryFacade)
        {
            _priceInqueryQueryFacade = priceInqueryQueryFacade;
        }

        [HttpGet]
        public Task<ProperPriceRequestQuestion> GetProperPriceForRequest([FromQuery] long requestId)
        {
            return _priceInqueryQueryFacade.GetProperPriceForRequest(requestId);
        }

        [HttpGet]
        public Task<ProperPriceSubjectQuestion> GetProperPriceForSubject([FromQuery] long subjectId)
        {
            return _priceInqueryQueryFacade.GetProperPriceForSubject(subjectId);
        }
    }
}
