using OltivaFlix.Domain.Model;
using System.Threading.Tasks;

namespace OltivaFlix.Domain.Service
{
    public interface IMovieServiceClient
    {
        public Task<Movie[]> SearchMovies(string query);

        public Task<Movie> GetMovie(string id);
    }
}