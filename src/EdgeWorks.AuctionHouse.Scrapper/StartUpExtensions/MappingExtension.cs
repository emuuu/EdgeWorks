using AutoMapper;
using EdgeWorks.AuctionHouse.Scraper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace EdgeWorks.AuctionHouse.Scraper.StartUpExtensions
{
    public static class MappingExtension
    {
        /// <summary>	An IServiceCollection extension method that configures mappping. </summary>
        /// <param name="services">			The services to act on. </param>
        /// <param name="configuration">	The configuration. </param>
        /// <returns>	An IServiceCollection. </returns>
        public static IServiceCollection ConfigureMapping(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddAutoMapper(c => c.AddProfile<MappingProfile>(), typeof(Startup));

            return services;
        }
    }
}
