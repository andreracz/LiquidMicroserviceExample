using MediatR;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Queries
{
    public class ListMoviesResponse : IRequest
    {
        public Movie[] Movies { get; set; }
    }
}