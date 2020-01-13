using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using EdgeWorks.AuctionHouse.Scraper.StartUpExtensions;
using Microsoft.AspNetCore.Routing.Constraints;
using Hangfire;
using System;
using Microsoft.Extensions.Hosting;
using EdgeWorks.AuctionHouse.Scraper.Services;
using Hangfire.Storage;

namespace EdgeWorks.AuctionHouse.Scraper
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var appSettings = "appsettings.json";
            var appSettingsSecret = "appsettings.secret.json";

            //Preparation for hosting in Docker
            //see docker-compose.yml for mounting a volume to configPath
            if (!env.IsDevelopment())
            {
                var settingsPath = @"C:\config\";
                if (System.IO.File.Exists(settingsPath + appSettings))
                {
                    appSettings = settingsPath + appSettings;
                    appSettingsSecret = settingsPath + appSettingsSecret;
                }
            }
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(appSettings, optional: true, reloadOnChange: true)
                .AddJsonFile(appSettingsSecret, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureLogging(Configuration)
                .ConfigureMapping(Configuration)
                .ConfigureHangfire(Configuration)
                .ConfigureDataServices(Configuration)
                .ConfigureBlizzardApis(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHangfire(env);
        }
    }
}
