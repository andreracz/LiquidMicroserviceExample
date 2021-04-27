using AutoMapper;
using Liquid.Cache;
using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Core.Utils;
using Liquid.Services.Configuration;
using Liquid.Services.Http;
using Microsoft.Extensions.Logging;
using OltivaFlix.Domain.Exceptions;
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
                key: $"MovieId:{id}",
                action: () => GetAsync<Movie>(endpoint: $"?apikey=2f93d90d&i={id}").Result,
                TimeSpan.FromMinutes(5));

            Movie result = null;

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                result = await response.GetContentObjectAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Movie>> SearchMovies(string query)
        {
            var result = await _lightCache.RetrieveOrAddAsync(
                key: $"MovieName:{query}",
                action: () =>
                {
                    var httpResponse = GetAsync<SearchResult>($"?apikey=2f93d90d&s={query}").Result;

                    if (httpResponse.HttpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.GetContentObjectAsync().Result;

                        return result.Search;
                    }

                    return null;
                },
                expirationDuration: TimeSpan.FromMinutes(5));

            if (result is null)
            {
                throw new MovieNotFoundException();
            }

            return result;
        }
    }
}