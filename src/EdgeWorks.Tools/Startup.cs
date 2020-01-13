using EdgeWorks.Tools.App;
using EdgeWorks.Tools.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EdgeWorks.Tools
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder();
            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AnalyseService>();


            // IMPORTANT! Register application entry point
            services.AddTransient<Application>();
        }

    }
}
