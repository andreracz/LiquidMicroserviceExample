
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Liquid.Services.Http;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Core.Configuration;
using Liquid.Services.Configuration;
using AutoMapper;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Service;

namespace OltivaFlix.Infrastructure.ServiceClient {

    public class MovieServiceHttpClient : LightHttpService, IMovieServiceClient
    {

        public MovieServiceHttpClient(IHttpClientFactory httpClientFactory, 
                           ILoggerFactory loggerFactory, 
                           ILightContextFactory contextFactory, 
                           ILightTelemetryFactory telemetryFactory, 
                           ILightConfiguration<List<LightServiceSetting>> servicesSettings, 
                           IMapper mapperService) : base(httpClientFactory, loggerFactory, contextFactory, telemetryFactory, servicesSettings, mapperService)
        {
        }

        public async Task<Movie[]>  SearchMovies(string query)
        {
            var response = await GetAsync<SearchResult>("?apikey=2f93d90d&s=" + query);

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                var result = await response.GetContentObjectAsync();
                return result.Search;
            }
            return await Task.FromResult<Movie[]>(new Movie[]{});
        }
    }

    class SearchResult {
        public Movie[] Search {get;set;}
        public int TotalResults {get;set;}
        public bool Result {get;set;}
    }
}