using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MozaeekTechnicianProfile.ApiCall
{
    public static class ApiAggregatorHttpClientConfiguration
    {
        private const string MediationServiceUrls = "MediationServiceUrls";
        public const string UserProfile = "UserProfile";


        public static void AddApiAggregatorHttpClient(this IServiceCollection services, IConfigurationRoot configuration)
        {
            AddMediationHttpClient(services, configuration, UserProfile);
        }

        private static void AddMediationHttpClient(IServiceCollection services, IConfigurationRoot configuration, string providerName)
        {
            services.AddHttpClient(providerName,
                    c =>
                    {
                        c.BaseAddress = new Uri(configuration[($"{MediationServiceUrls}:{providerName}")]);
                    })
                .ConfigurePrimaryHttpMessageHandler(ConfigureThirdPartyHanler);
        }

        private static HttpMessageHandler ConfigureThirdPartyHanler()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }

            return handler;
        }


    }
}
