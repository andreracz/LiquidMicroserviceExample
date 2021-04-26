using AutoMapper;
using Liquid.Cache;
using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Services.Configuration;
using Liquid.Services.Http;
using Microsoft.Extensions.Logging;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OltivaFlix.Infrastructure.ServiceClient
{
    public class MovieServiceHttpClient : LightHttpService, IMovieServiceClient
    {
        private readonly ILightCache _lightCache;
        public MovieServiceHttpClient(IHttpClientFactory httpClientFactory,
                           ILoggerFactory loggerFactory,
                           ILightContextFactory contextFactory,
                           ILightTelemetryFactory telemetryFactory,
                           ILightConfiguration<List<LightServiceSetting>> servicesSettings,
                           IMapper mapperService,
                           ILightCache lightCache)
            : base(httpClientFactory,
                  loggerFactory,
                  contextFactory,
                  telemetryFactory,
                  servicesSettings,
                  mapperService)
        {
            _lightCache = lightCache;
        }

        public async Task<Movie> GetMovie(string id)
        {
            var response = await _lightCache.RetrieveOrAddAsync(
               key: "MovieId",
                action: () => GetAsync<Movie>(endpoint: $"?apikey=2f93d90d&i={id}").Result,
                TimeSpan.FromMinutes(5));            

            Movie result = null;

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                result = await response.GetContentObjectAsync();
            }

            return result;
        }

        public async Task<Movie[]> SearchMovies(string query)
        {
            var response = await _lightCache.RetrieveOrAddAsync(
               key: "MovieId",
                action: () => GetAsync<SearchResult>(endpoint: $"?apikey=2f93d90d&i={query}").Result,
                TimeSpan.FromMinutes(5));

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                var result = await response.GetContentObjectAsync();

                return result.Search;
            }

            return new Movie[] { };
        }
    }
}