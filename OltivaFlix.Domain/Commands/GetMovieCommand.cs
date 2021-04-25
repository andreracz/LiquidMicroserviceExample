
using System;
using MediatR;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Command
{
    public class GetMovieCommand: IRequest<Movie>
    {
        public string ImdbId { get; set;}
    }
}
