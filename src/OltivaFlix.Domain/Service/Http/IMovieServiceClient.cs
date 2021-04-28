using OltivaFlix.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OltivaFlix.Domain.Service
{
    public interface IMovieServiceClient
    {
        public Task<IEnumerable<Movie>> SearchMovies(string query);

        public Task<Movie> GetMovie(string id);
    }
}