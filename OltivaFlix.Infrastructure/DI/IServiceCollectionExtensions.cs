using Microsoft.Extensions.DependencyInjection;
using Liquid.Services.Http;

namespace OltivaFlix.Infrastructure.DI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterHttpService(this IServiceCollection services)
        {
            services.AddHttpServices(typeof(IServiceCollectionExtensions).Assembly);

            return services;
        }

        
    }
}
