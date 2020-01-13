using EdgeWorks.Tools.App;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EdgeWorks.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Application>().Run();
        }
    }
}
