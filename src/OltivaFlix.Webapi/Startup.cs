using Liquid.Cache.Redis;
using Liquid.Core.DependencyInjection;
using Liquid.Messaging.Extensions;
using Liquid.WebApi.Http.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OltivaFlix.Domain.Config;
using OltivaFlix.Domain.DI;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Service;
using OltivaFlix.Infra.MessageBroker;
using OltivaFlix.Infra.ServiceClient;
using OltivaFlix.Infra.ServiceClient.DI;

namespace OltivaFlix.Webapi
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
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

            services
                .AddOptions();

            services
               .Configure<CacheConfig>(_configuration.GetSection(nameof(CacheConfig)));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMovieServiceClient, MovieServiceHttpClient>();
            services.AddAutoMapper(typeof(ListMoviesQuery).Assembly);

            services.ConfigureLiquidHttp();
            services.AddLiquidSwagger();
            services.AddLightRedisCache();
            //services.AddLightMemoryCache();
            services.RegisterDomainConfigs();
            services.RegisterHttpService();

            services.RegisterMessage();
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

            app.ApplicationServices.StartProducersConsumers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}