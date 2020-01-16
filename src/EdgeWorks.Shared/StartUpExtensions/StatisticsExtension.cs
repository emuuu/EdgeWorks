using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Data.Statistics;

namespace EdgeWorks.Shared.StartUpExtensions
{
	public static class StatisticsExtension
	{
		/// <summary>	An IServiceCollection extension method that configures data services. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureStatistics(this IServiceCollection services, IConfigurationRoot configuration)
		{
			//add Sqlite-DataLayer
			services.AddSingleton<StatisticServiceOptions>();
			services.AddScoped<StatisticDataService>();

			return services;
		}
	}
}