using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Data.System;

namespace EdgeWorks.Shared.StartUpExtensions
{
	public static class FileStorageExtensions
	{
		/// <summary>	An IServiceCollection extension method that configures data services. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureFileStorage(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.AddSingleton<FileServiceOptions>();
			services.AddScoped<FileDataService>();
			services.AddScoped<IFileService, LocalFileService>();

			return services;
		}
	}
}