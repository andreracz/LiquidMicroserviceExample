using Liquid.Cache.Memory;
using Liquid.Cache.Redis;
using Liquid.Core.DependencyInjection;
using Liquid.WebApi.Http.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OltivaFlix.Domain.DI;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Service;
using OltivaFlix.Infrastructure.DI;
using OltivaFlix.Infrastructure.ServiceClient;

namespace OltivaFlix.Webapi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCache(services);

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services
                .Configure<GzipCompressionProviderOptions>(options =>
                    options.Level = System.IO.Compression.CompressionLevel.Optimal)
                .AddResponseCompression(options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                    options.EnableForHttps = true;
                });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMovieServiceClient, MovieServiceHttpClient>();
            services.AddAutoMapper(typeof(ListMoviesQuery).Assembly);

            services.ConfigureLiquidHttp();
            services.AddLiquidSwagger();


            //

            services.RegisterDomainRequestHandler();
            services.RegisterHttpService();

        }

        private void ConfigureCache(IServiceCollection services)
        {
            string cacheType = _configuration["OltivaCache:CacheType"];

            switch (cacheType.ToUpper())
            {
                case "REDIS":
                    services.AddLightRedisCache();
                    break;
                default:
                    services.AddLightMemoryCache();
                    break;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLiquidSwagger();
            app.ConfigureApplication();

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}