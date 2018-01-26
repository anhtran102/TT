using System;

namespace Tieptuyen.Components.Core.Api.Services
{
    /// <summary>
    /// Provides functionality for working with dates and times.
    /// </summary>
    public interface IDateService
    {
        /// <summary>
        /// Gets the current date.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> structure representing the current date.</returns>
        /// <remarks>The time portion is always set to <c>00:00:00</c>.</remarks>
        DateTime GetCurrentDate();

        /// <summary>
        /// Gets the current date time.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> structure representing the current date and time.</returns>
        DateTime GetCurrentDateTime();

        /// <summary>
        /// Gets the current UTC date time.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> structure representing the current UTC date and time.</returns>
        DateTime GetCurrentUtcDateTime();
    }
}
