using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Shared.Configurations.BlizzardAPIs;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.BattleNet;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.CommunityAPIs;

namespace EdgeWorks.AuctionHouse.Scraper.StartUpExtensions
{
	public static class BlizzardApiExtensions
	{
		/// <summary>	An IServiceCollection extension method that configures Blizzard services. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureBlizzardApis(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
			services.Configure<BlizzardClient>(configuration.GetSection("BlizzardClient"));

			//add apis
			services.AddSingleton<OAuthApi>();
			services.AddSingleton<AuctionAPI>();

			return services;
		}
	}
}