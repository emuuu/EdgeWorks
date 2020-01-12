using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EdgeWorks.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOption<T>(this IServiceCollection services, IConfigurationRoot configuration) where T : class
        {
            services.Configure<T>(configuration.GetSection(typeof(T).Name));
        }
    }
}
