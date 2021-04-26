using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Service;
using Liquid.Domain;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Cache;
using AutoMapper;

namespace OltivaFlix.Domain.Handler
{
    public class GetMovieCommandHandler : RequestHandlerBase, IRequestHandler<GetMovieQuery, Movie>
    {


        private readonly IMovieServiceClient _movieService;
        private readonly ILightCache _cache;

        public GetMovieCommandHandler(IMediator mediatorService,
                                        ILightContext contextService,
                                        ILightTelemetry telemetryService,
                                        IMapper mapperService,
                                        IMovieServiceClient movieService,
                                        ILightCache cache) : base(mediatorService, contextService, telemetryService, mapperService)
        {
            _movieService = movieService;
            _cache = cache;
        }

        public async Task<Movie> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            return await _cache.RetrieveOrAddAsync($"MOVIE:{request.ImdbId}", () => _movieService.GetMovie(request.ImdbId).Result, expirationDuration: System.TimeSpan.FromMinutes(10));
        }
    }
}