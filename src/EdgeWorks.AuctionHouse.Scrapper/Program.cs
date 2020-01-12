using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace EdgeWorks.AuctionHouse.Scraper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "EdgeWorks AuctionHouse-Scraper";

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
