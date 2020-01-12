using EdgeWorks.AuctionHouse.Scraper.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EdgeWorks.AuctionHouse.Scraper.StartUpExtensions
{
    public static class HostingExtension
    {
		/// <summary>	An IServiceCollection extension method that configures hangfire. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.AddHangfire(config =>
			{
				config.UseMemoryStorage();
			});

			return services;
		}

		/// <summary>	An IApplicationBuilder extension method that initializes hangfire and its jobs. </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="environment">  	The environment. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireServer();

			//define recurring jobs
			RecurringJob.AddOrUpdate<ScrapeService>("Auction API scraping", x => x.GetAuctions(), Cron.MinuteInterval(5));

			return app;
        }
    }

}
