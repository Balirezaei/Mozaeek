using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekUserProfile.Domain.OptionSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekUserProfile.RestAPI.Extensions
{
    public static class SettingExtensions
    {
        public static void ConfigureOptionSettings(this IServiceCollection service,IConfiguration configuration)
        {
            service.Configure<MicroserviceUrlsSettings>(configuration.GetSection(MicroserviceUrlsSettings.Name));
        }
    }
}
