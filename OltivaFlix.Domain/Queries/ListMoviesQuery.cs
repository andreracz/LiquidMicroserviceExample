
using System;
using MediatR;

namespace OltivaFlix.Domain.Queries
{
    public class ListMoviesQuery: IRequest<ListMoviesResponse>
    {
        public string SearchString { get; set;}
    }
}
