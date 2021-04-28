using Microsoft.Extensions.DependencyInjection;
using Liquid.Domain.Extensions;

namespace OltivaFlix.Domain.DI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainRequestHandler(this IServiceCollection services)
        {
            services.AddDomainRequestHandlers(typeof(IServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}