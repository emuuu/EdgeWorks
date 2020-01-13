using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Shared.Configurations.BlizzardAPIs;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.BattleNet;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.CommunityAPIs;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.WorldOfWarcraft.GameDataAPIs;

namespace EdgeWorks.AuctionHouse.Shared.StartUpExtensions
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
			services.AddSingleton<ItemAPI>();

			return services;
		}
	}
}