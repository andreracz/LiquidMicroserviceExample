using AutoMapper;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Domain;
using MediatR;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Service;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OltivaFlix.Domain.Handler
{
    public class ListMoviesCommandHandler : RequestHandlerBase, IRequestHandler<ListMoviesQuery, IEnumerable<Movie>>
    {
        private readonly IMovieServiceClient _movieService;

        public ListMoviesCommandHandler(IMediator mediatorService,
                                        ILightContext contextService,
                                        ILightTelemetry telemetryService,
                                        IMapper mapperService,
                                        IMovieServiceClient movieService)
            : base(mediatorService,
                  contextService,
                  telemetryService,
                  mapperService)
        {
            _movieService = movieService;
        }

        public async Task<IEnumerable<Movie>> Handle(ListMoviesQuery request, CancellationToken cancellationToken)
        {
            return  await _movieService.SearchMovies(request.SearchString);
        }
    }
}