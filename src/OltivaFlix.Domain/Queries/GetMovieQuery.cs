using MediatR;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Queries
{
    public class GetMovieQuery : IRequest<Movie>
    {
        public string ImdbId { get; set; }
    }
}