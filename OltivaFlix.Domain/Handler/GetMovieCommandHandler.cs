using AutoMapper;
using Liquid.Cache;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Domain;
using MediatR;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Service;
using System.Threading;
using System.Threading.Tasks;

namespace OltivaFlix.Domain.Handler
{
    public class GetMovieCommandHandler : RequestHandlerBase, IRequestHandler<GetMovieQuery, MovieResponse>
    {
        private readonly IMovieServiceClient _movieService;
        private readonly ILightCache _cache;

        public GetMovieCommandHandler(IMediator mediatorService,
                                      ILightContext contextService,
                                      ILightTelemetry telemetryService,
                                      IMapper mapperService,
                                      IMovieServiceClient movieService,
                                      ILightCache cache)
            : base(mediatorService,
                  contextService,
                  telemetryService,
                  mapperService)
        {
            _movieService = movieService;
            _cache = cache;
        }

        public async Task<MovieResponse> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            return new MovieResponse()
            {
                Movie = await _movieService.GetMovie(request.ImdbId)
            };
        }
    }
}