using EdgeWorks.AuctionHouse.Scraper.Configuration;
using EdgeWorks.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace EdgeWorks.AuctionHouse.Scraper.StartUpExtensions
{
    public static class LoggingExtension
    {
        /// <summary>	An IServiceCollection extension method that configures logging. </summary>
        /// <param name="services">			The services to act on. </param>
        /// <param name="configuration">	The configuration. </param>
        /// <returns>	An IServiceCollection. </returns>
        public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var logConfig = configuration.GetSection("Logging").Get<Logging>();
            if (logConfig.WriteToFile)
            {
                var logPath = Path.GetDirectoryName(logConfig.FilePath);
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
            }

            var serilogLogger = new LoggerConfiguration()
                .WriteTo.Logger(lc =>
                {
                    if (logConfig.WriteToFile)
                        lc.WriteTo.File(logConfig.FilePath);

                    if (logConfig.WriteToConsole)
                        lc.WriteTo.Console();
                })
                .CreateLogger();


            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(logConfig.MinimumLogLevel);
                builder.AddSerilog(logger: serilogLogger, dispose: true);
            });
            
            return services;
        }
    }
}
