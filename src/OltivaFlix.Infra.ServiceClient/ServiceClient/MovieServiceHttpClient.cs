using AutoMapper;
using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Messaging;
using Liquid.Services.Configuration;
using Liquid.Services.Http;
using Microsoft.Extensions.Logging;
using OltivaFlix.Domain.Messages.Publishers;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OltivaFlix.Infra.ServiceClient
{
    public class MovieServiceHttpClient : LightHttpService, IMovieServiceClient
    {
        public MovieServiceHttpClient(IHttpClientFactory httpClientFactory,
                           ILoggerFactory loggerFactory,
                           ILightContextFactory contextFactory,
                           ILightTelemetryFactory telemetryFactory,
                           ILightConfiguration<List<LightServiceSetting>> servicesSettings,
                           IMapper mapperService)
            : base(httpClientFactory,
                  loggerFactory,
                  contextFactory,
                  telemetryFactory,
                  servicesSettings,
                  mapperService)
        {
            
        }

        public async Task<Movie> GetMovie(string id)
        {
            var response = await GetAsync<Movie>(endpoint: $"?apikey=2f93d90d&i={id}");

            Movie result = null;

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                result = await response.GetContentObjectAsync();

                if (result.Response.ToUpper().Equals(bool.FalseString.ToUpper()))
                    return null;                
            }

            return result;
        }

        public async Task<IEnumerable<Movie>> SearchMovies(string query)
        {
            SearchResult result = null;
            var httpResponse = await GetAsync<SearchResult>($"?apikey=2f93d90d&s={query}");

            if (httpResponse.HttpResponse.IsSuccessStatusCode)
            {
                result = await httpResponse.GetContentObjectAsync();

                return result.Search;
            }

            return null;
        }
    }
}