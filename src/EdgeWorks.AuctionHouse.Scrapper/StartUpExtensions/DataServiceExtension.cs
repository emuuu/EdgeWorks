using EdgeWorks.AuctionHouse.Scraper.Services;
using EdgeWorks.AuctionHouse.Scraper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Data;
using EdgeWorks.Data.Configurations.SqliteOptions;
using EdgeWorks.Shared.Services.Authentication;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Shared.Configuration;

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
			//add configurations
			services.AddSingleton<IConfiguration>(configuration);
			services.Configure<ServerPaths>(configuration.GetSection("ServerPaths"));

			//add Sqlite-DataLayer
			services.AddSingleton<AuctionServiceOptions>();
			services.AddScoped<AuctionDataService>();
			services.AddSingleton<FileServiceOptions>();
			services.AddScoped<FileDataService>();

			//add shared services
			services.AddSingleton<ITokenService, TokenService>();

			services.AddScoped<IFileService, LocalFileService>();
			//services.AddScoped<IFileService, OneDriveService>();

			//add services
			services.AddScoped<ScrapeService>();

			return services;
		}
	}
}