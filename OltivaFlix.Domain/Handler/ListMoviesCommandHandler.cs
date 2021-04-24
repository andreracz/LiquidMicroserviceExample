
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OltivaFlix.Domain.Command;
using OltivaFlix.Domain.Service;
using Liquid.Domain;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using AutoMapper;

namespace OltivaFlix.Domain.Handler
{
    public class ListMoviesCommandHandler : RequestHandlerBase, IRequestHandler<ListMoviesCommand, ListMoviesResponse>
    {


       // private readonly IMovieServiceClient _movieService;
        public ListMoviesCommandHandler(IMediator mediatorService, 
                                        ILightContext contextService, 
                                        ILightTelemetry telemetryService, 
                                        IMapper mapperService) : base(mediatorService, contextService, telemetryService, mapperService)
        {
            // _movieService = movieService;
        }

        public async Task<ListMoviesResponse> Handle(ListMoviesCommand request, CancellationToken cancellationToken)
        {
            var response = new ListMoviesResponse() {
                Movies = new Model.Movie[] {
                    new Model.Movie() {
                        Title = "Teste"
                    }
                }
            };
            return await Task.FromResult<ListMoviesResponse>(response);
        }
    }
}
