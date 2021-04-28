using Liquid.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OltivaFlix.Domain.DI
{
    public static class DomainRequestHandlers
    {
        public static IServiceCollection RegisterDomainConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainRequestHandlers(typeof(DomainRequestHandlers).Assembly);

            //services
            //    .Configure<CacheConfig>(configuration.GetSection(nameof(CacheConfig)));
            return services;
        }
    }
}