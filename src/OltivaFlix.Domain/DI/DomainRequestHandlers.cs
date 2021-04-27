using Microsoft.Extensions.DependencyInjection;
using Liquid.Domain.Extensions;

namespace OltivaFlix.Domain.DI
{
    public static class DomainRequestHandlers
    {
        public static IServiceCollection RegisterDomainRequestHandler(this IServiceCollection services)
        {
            services.AddDomainRequestHandlers(typeof(DomainRequestHandlers).Assembly);

            return services;
        }
    }
}