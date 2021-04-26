using Newtonsoft.Json;

namespace OltivaFlix.Domain.Model
{
    public partial class Rating
    {
        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}