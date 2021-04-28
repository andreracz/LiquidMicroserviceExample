using AutoMapper;
using Liquid.Cache;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Domain;
using MediatR;
using Microsoft.Extensions.Options;
using OltivaFlix.Domain.Config;
using OltivaFlix.Domain.Exceptions;
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
        private readonly CacheConfig _cacheConfig;

        public GetMovieCommandHandler(IMediator mediatorService,
                                      ILightContext contextService,
                                      ILightTelemetry telemetryService,
                                      IMapper mapperService,
                                      IMovieServiceClient movieService,
                                      ILightCache cache,
                                      IOptions<CacheConfig> cacheConfig)
            : base(mediatorService,
                  contextService,
                  telemetryService,
                  mapperService)
        {
            _movieService = movieService;
            _cache = cache;
            _cacheConfig = cacheConfig.Value;
        }

        public async Task<Movie> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            var result = await _cache.RetrieveOrAddAsync<Movie>(
               key: $"MovieId:{request.ImdbId}",
               action: () =>
               {
                   return _movieService.GetMovie(request.ImdbId).Result;
               },
               expirationDuration: System.TimeSpan.FromMinutes(_cacheConfig.CacheTTLInMinutes));

            if (result is null)
            {
                throw new MovieNotFoundException();
            }

            return result;
        }
    }
}