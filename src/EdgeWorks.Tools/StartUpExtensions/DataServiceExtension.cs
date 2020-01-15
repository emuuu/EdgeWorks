using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Shared.Services.Authentication;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Shared.Configuration;
using EdgeWorks.Tools.Services;
using EdgeWorks.Tools.Configuration;

namespace EdgeWorks.Tools.StartUpExtensions
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
			services.Configure<ObservedItems>(configuration.GetSection("ObservedItems"));

			//add Sqlite-DataLayer

			//add shared services
			services.AddSingleton<ITokenService, TokenService>();
			services.AddSingleton<IFileService, LocalFileService>();

			//add services
			services.AddSingleton<AnalyseService>();

			return services;
		}
	}
}