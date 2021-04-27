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
        {
        }

        [HttpGet(template: "GetByName/{searchString}")]
        public async Task<IActionResult> SearchMovies(string searchString) =>
            await ExecuteAsync(new ListMoviesQuery() { SearchString = searchString });

        [HttpGet(template: "GetById/{id}")]
        public async Task<IActionResult> GetMovie(string id) =>
            await ExecuteAsync(new GetMovieQuery() { ImdbId = id });
    }
}