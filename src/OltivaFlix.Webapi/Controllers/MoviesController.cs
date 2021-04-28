using Liquid.Core.Context;
using Liquid.Core.Localization;
using Liquid.Core.Telemetry;
using Liquid.WebApi.Http.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OltivaFlix.Domain.Queries;
using System.Threading.Tasks;

namespace OltivaFlix.Webapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MoviesController : BaseController
    {
        public MoviesController(ILoggerFactory loggerFactory,
                                  IMediator mediator,
                                  ILightContext context,
                                  ILightTelemetry telemetry,
                                  ILocalization localization)
            : base(loggerFactory,
                  mediator,
                  context,
                  telemetry,
                  localization)
        { } 

        [HttpGet()]
        public async Task<IActionResult> SearchMovies([FromQuery(Name="nameSearch")] string nameSearch) =>
            await ExecuteAsync(new ListMoviesQuery() { SearchString = nameSearch });

        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetMovie(string id) =>
            await ExecuteAsync(new GetMovieQuery() { ImdbId = id });
    }
}