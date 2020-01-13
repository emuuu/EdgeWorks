using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Shared.Configuration;

namespace EdgeWorks.Shared.StartUpExtensions
{
	public static class ConfigurationExtension
	{
		/// <summary>	An IServiceCollection extension method that configures data services. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection AddBasicConfigurations(this IServiceCollection services, IConfigurationRoot configuration)
		{
			//add configurations
			services.AddSingleton<IConfiguration>(configuration);
			services.Configure<ServerPaths>(configuration.GetSection("ServerPaths"));

			return services;
		}
	}
}