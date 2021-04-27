using Microsoft.Extensions.DependencyInjection;
using Liquid.Core.Configuration;
using Liquid.Domain.Extensions;
using OltivaFlix.Domain.Config;

namespace OltivaFlix.Domain.DI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainRequestHandler(this IServiceCollection services)
        {
            services.AddDomainRequestHandlers(typeof(IServiceCollectionExtensions).Assembly);

            return services;
        }

        public static IServiceCollection RegisterCacheConfig(this IServiceCollection services)
        {
            
            services.AddSingleton<ILightConfiguration<CacheSettings>, CacheConfiguration>();
            return services;
        }
    }

     
}