using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Shared.Configurations.BlizzardAPIs;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.BattleNet;
using EdgeWorks.Shared.Configurations.MicrosoftAPIs;

namespace EdgeWorks.AuctionHouse.Scraper.StartUpExtensions
{
	public static class MicrosoftApiExtensions
	{
		/// <summary>	An IServiceCollection extension method that configures Blizzard services. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureMicrosoftApis(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.Configure<MicrosoftClient>(configuration.GetSection("MicrosoftClient"));

			return services;
		}
	}
}