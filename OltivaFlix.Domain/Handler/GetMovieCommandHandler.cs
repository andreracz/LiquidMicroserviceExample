
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OltivaFlix.Domain.Command;
using OltivaFlix.Domain.Model;
using OltivaFlix.Domain.Service;
using Liquid.Domain;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Cache;
using AutoMapper;

namespace OltivaFlix.Domain.Handler
{
    public class GetMovieCommandHandler : RequestHandlerBase, IRequestHandler<GetMovieCommand, Movie>
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

        public async Task<Movie> Handle(GetMovieCommand request, CancellationToken cancellationToken)
        {
            var cachedResponse = await _cache.RetrieveAsync<Movie>("MOVIE:" + request.ImdbId);
            if (cachedResponse == null) {
                this.TelemetryService.AddInformationTelemetry("Cache miss");
                var response = await _movieService.GetMovie(request.ImdbId);
                await _cache.AddAsync("MOVIE:" + request.ImdbId, response, TimeSpan.FromMinutes(10));
                return response;
            }
            this.TelemetryService.AddInformationTelemetry("Retrieved from cache");
            return cachedResponse;
        }
    }
}
