using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Liquid.Core.Context;
using Liquid.Core.Localization;
using Liquid.Core.Telemetry;
using Liquid.WebApi.Http.Controllers;
using OltivaFlix.Domain.Command;
using MediatR;

namespace OltivaFlix.Webapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MoviesController  : BaseController
    {


        public MoviesController(ILoggerFactory loggerFactory, 
                                  IMediator mediator, 
                                  ILightContext context, 
                                  ILightTelemetry telemetry, 
                                  ILocalization localization) : base(loggerFactory, mediator, context, telemetry, localization)
        {
        }

        [HttpGet]
        public async Task<IActionResult> SearchMovies(string searchString) => await ExecuteAsync(new ListMoviesCommand() { SearchString = searchString});

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(string id) => await ExecuteAsync(new GetMovieCommand() { ImdbId = id});


    }
}
