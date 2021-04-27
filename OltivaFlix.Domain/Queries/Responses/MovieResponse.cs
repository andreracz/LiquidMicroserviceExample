using MediatR;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Queries
{
    public class MovieResponse : IRequest
    {
        public Movie Movie { get; set; }
    }
}