using Liquid.Core.Exceptions;

namespace OltivaFlix.Domain.Exceptions
{
    /// <summary>
    /// Occurs when the movie searched is not found.
    /// </summary>
    /// <seealso cref="Liquid.Core.Exceptions.LightCustomException" />
    public class MovieNotFoundException : LightCustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieNotFoundException"/> class.
        /// </summary>
        public MovieNotFoundException() : base("Movie not found.", ExceptionCustomCodes.NotFound)
        {
        }
    }
}