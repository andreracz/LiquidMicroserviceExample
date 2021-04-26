using OltivaFlix.Domain.Model;

namespace OltivaFlix.Infrastructure.ServiceClient
{
    internal class SearchResult
    {
        public Movie[] Search { get; set; }
        public int TotalResults { get; set; }
        public bool Result { get; set; }
    }
}