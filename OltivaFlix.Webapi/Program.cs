using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Liquid.Core.Configuration;
using Liquid.WebApi.Http.Configuration;
using Liquid.WebApi.Http;
using Liquid.WebApi.Http.Extensions;

using Microsoft.Extensions.Logging;

namespace OltivaFlix.Webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(new ConfigurationBuilder().AddLightConfigurationFile().Build());
                    //webBuilder.AddLightConfigurationFile();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
