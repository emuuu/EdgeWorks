using EdgeWorks.AuctionHouse.Shared.StartUpExtensions;
using EdgeWorks.Shared.StartUpExtensions;
using EdgeWorks.Tools.App;
using EdgeWorks.Tools.StartUpExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EdgeWorks.Tools
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var appSettings = "appsettings.json";
            var appSettingsSecret = "appsettings.secret.json";

            var builder = new ConfigurationBuilder()
                .AddJsonFile(appSettings, optional: true, reloadOnChange: true)
                .AddJsonFile(appSettingsSecret, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddBasicConfigurations(Configuration)
                .ConfigureFileStorage(Configuration)
                .ConfigureLogging(Configuration)
                .ConfigureDataServices(Configuration)
                .ConfigureBlizzardApis(Configuration);

            // IMPORTANT! Register application entry point
            services.AddTransient<Application>();
        }

    }
}
