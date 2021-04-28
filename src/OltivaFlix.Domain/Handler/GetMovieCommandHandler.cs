using AutoMapper;
using Liquid.Cache;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Domain;
using Liquid.Messaging;
using MediatR;
using Microsoft.Extensions.Options;
using OltivaFlix.Domain.Config;
using OltivaFlix.Domain.Exceptions;
using OltivaFlix.Domain.Messages.Publishers;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Service;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OltivaFlix.Domain.Handler
{
    public class GetMovieCommandHandler : RequestHandlerBase, IRequestHandler<GetMovieQuery, Movie>
    {
        private readonly IMovieServiceClient _movieService;
        private readonly ILightCache _cache;
        private readonly CacheConfig _cacheConfig;
        private readonly ILightProducer<MovieAudience> _lightProducer;

        public GetMovieCommandHandler(IMediator mediatorService,
                                      ILightContext contextService,
                                      ILightTelemetry telemetryService,
                                      IMapper mapperService,
                                      IMovieServiceClient movieService,
                                      ILightCache cache,
                                      IOptions<CacheConfig> cacheConfig,
                                      ILightProducer<MovieAudience> lightProducer)
            : base(mediatorService,
                  contextService,
                  telemetryService,
                  mapperService)
        {
            _movieService = movieService;
            _cache = cache;
            _cacheConfig = cacheConfig.Value;
            _lightProducer = lightProducer;
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
            else
            {
                await _lightProducer.SendMessageAsync(
                    message: new MovieAudience { Id = result.ImdbId, MovieName = result.Title },
                    customHeaders: new Dictionary<string, object>());
            }

            return result;
        }
    }
}