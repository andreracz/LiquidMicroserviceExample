
using System;
using MediatR;

namespace OltivaFlix.Domain.Command
{
    public class ListMoviesCommand: IRequest<ListMoviesResponse>
    {
        public string SearchString { get; set;}
    }
}
