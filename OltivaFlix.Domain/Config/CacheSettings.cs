

namespace OltivaFlix.Domain.Config
{
    public class CacheSettings
    {
        public const string CacheConfigKey = "CacheConfig";

        public int CacheTimeMinutes { get; set; }

        public CacheType CacheType { get; set; } 
    }
}
