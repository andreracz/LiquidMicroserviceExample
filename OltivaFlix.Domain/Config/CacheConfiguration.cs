using System.Diagnostics.CodeAnalysis;
using Liquid.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace OltivaFlix.Domain.Config
{
    /// <summary>
    /// Redis Cache Configuration Class.
    /// </summary>
    /// <seealso>
    ///     <cref>Liquid.Core.Configuration.ILightConfiguration{Liquid.Cache.Redis.Configuration.RedisCacheSettings}</cref>
    /// </seealso>
    /// <seealso>
    ///     <cref>Liquid.Configuration.AppSetting</cref>
    /// </seealso>
    [ExcludeFromCodeCoverage]
    [ConfigurationSection("OltivaCache")]
    public class CacheConfiguration : LightConfiguration, ILightConfiguration<CacheSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightRedisCacheConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CacheConfiguration(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public CacheSettings Settings => GetConfigurationSection<CacheSettings>();
    }
}