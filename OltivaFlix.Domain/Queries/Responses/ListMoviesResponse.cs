using MediatR;
using OltivaFlix.Domain.Model;
using System.Collections.Generic;

namespace OltivaFlix.Domain.Queries
{
    public class ListMoviesResponse : IRequest
    {
        public ICollection<Movie> Movies { get; set; }
    }
}