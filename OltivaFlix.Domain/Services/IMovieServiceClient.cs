
using System;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Service
{
    public interface IMovieServiceClient
    {
        public Movie[] SearchMovies(string query);

    }
}
