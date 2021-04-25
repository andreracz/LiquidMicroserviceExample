
using System.Threading.Tasks;
using OltivaFlix.Domain.Model;

namespace OltivaFlix.Domain.Service
{
    public interface IMovieServiceClient
    {
        public Task<Movie[]> SearchMovies(string query);

    }
}
