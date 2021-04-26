using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Liquid.Core.DependencyInjection;
using Liquid.Domain.Extensions;
using Liquid.WebApi.Http.Extensions;
using Liquid.Services.Http;
using Liquid.Cache.Memory;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Handler;
using OltivaFlix.Domain.Service;
using OltivaFlix.Infrastructure.ServiceClient;

namespace OltivaFlix.Webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureLiquidHttp();
            services.AddLiquidSwagger();
            services.AddLightMemoryCache();
            services.AddAutoMapper(typeof(ListMoviesQuery).Assembly);
            services.AddDomainRequestHandlers(typeof(ListMoviesCommandHandler).Assembly);
            services.AddControllers();
            services.AddHttpServices(typeof(MovieServiceHttpClient).Assembly);
            services.AddSingleton<IMovieServiceClient,MovieServiceHttpClient>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // 
            }
            app.UseLiquidSwagger();
            app.ConfigureApplication();
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
