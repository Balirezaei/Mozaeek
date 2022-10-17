using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.RestAPI;
using Newtonsoft.Json;
using Xunit;

namespace MozaeekCore.IntegrationTest.Request
{
    public class RequestControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public RequestControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

      

    }
}