
using System;
using MediatR;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Command
{
    public class ListMoviesResponse: IRequest
    {
        public Movie[] Movies { get; set;}
    }
}
