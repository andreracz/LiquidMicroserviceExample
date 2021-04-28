using AutoMapper;
using Liquid.Cache;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Service;
using System.Threading;
using System.Threading.Tasks;

namespace OltivaFlix.Domain.Handler
{
    public class GetMovieCommandHandler : RequestHandlerBase, IRequestHandler<GetMovieQuery, Movie>
    {
        private readonly IMovieServiceClient _movieService;
        private readonly ILightCache _cache;

        private readonly IConfiguration _configuration;

        private readonly int _cacheMinutes;

        public GetMovieCommandHandler(IMediator mediatorService,
                                      ILightContext contextService,
                                      ILightTelemetry telemetryService,
                                      IMapper mapperService,
                                      IMovieServiceClient movieService,
                                      ILightCache cache,
                                      IConfiguration configuration)
            : base(mediatorService,
                  contextService,
                  telemetryService,
                  mapperService)
        {
            _movieService = movieService;
            _cache = cache;
            _configuration = configuration;

            _cacheMinutes = _configuration.GetValue<int>("OltivaCache:CacheTimeMinutes", defaultValue: 10);
        }

        public async Task<Movie> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            return await _cache.RetrieveOrAddAsync<Movie>(
               key: $"MovieId:{request.ImdbId}",
               action: () =>
               {
                   return _movieService.GetMovie(request.ImdbId).Result;
               },
               expirationDuration: System.TimeSpan.FromMinutes(_cacheMinutes));
        }
    }
}