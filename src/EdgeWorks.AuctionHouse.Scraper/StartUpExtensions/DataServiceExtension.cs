using EdgeWorks.AuctionHouse.Scraper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Shared.Services.Authentication;
using EdgeWorks.Data.Auctions;

namespace EdgeWorks.AuctionHouse.Scraper.StartUpExtensions
{
	public static class DataServiceExtension
	{
		/// <summary>	An IServiceCollection extension method that configures data services. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureDataServices(this IServiceCollection services, IConfigurationRoot configuration)
		{
			//add Sqlite-DataLayer
			services.AddSingleton<AuctionServiceOptions>();
			services.AddScoped<AuctionDataService>();

			//add shared services
			services.AddSingleton<ITokenService, TokenService>();

			//add services
			services.AddScoped<ScrapeService>();

			return services;
		}
	}
}