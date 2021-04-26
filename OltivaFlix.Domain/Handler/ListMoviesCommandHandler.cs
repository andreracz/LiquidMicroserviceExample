
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OltivaFlix.Domain.Queries;
using OltivaFlix.Domain.Service;
using Liquid.Domain;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using AutoMapper;

namespace OltivaFlix.Domain.Handler
{
    public class ListMoviesCommandHandler : RequestHandlerBase, IRequestHandler<ListMoviesQuery, ListMoviesResponse>
    {


        private readonly IMovieServiceClient _movieService;
        
        public ListMoviesCommandHandler(IMediator mediatorService, 
                                        ILightContext contextService, 
                                        ILightTelemetry telemetryService, 
                                        IMapper mapperService,
                                        IMovieServiceClient movieService) : base(mediatorService, contextService, telemetryService, mapperService)
        {
             _movieService = movieService;
        }

        public async Task<ListMoviesResponse> Handle(ListMoviesQuery request, CancellationToken cancellationToken)
        {
            var response = await _movieService.SearchMovies(request.SearchString);
            return new ListMoviesResponse() {
                Movies = response
            };
        }
    }
}
