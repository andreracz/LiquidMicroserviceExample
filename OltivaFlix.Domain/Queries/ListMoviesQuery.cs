using MediatR;
using System.Collections.Generic;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Queries
{
    public class ListMoviesQuery : IRequest<IEnumerable<Movie>>
    {
        public string SearchString { get; set; }
    }
}