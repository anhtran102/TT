using System;
using SystemConfigManager = System.Configuration.ConfigurationManager;

namespace Tieptuyen.Components.Core.Api.Util
{
    using System.Globalization;

    /// <summary>
    /// Provides extension methods for <see cref="System.DateTime" /> objects.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines whether the specified date/time has a time portion.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> has a time portion, otherwise <c>false</c>.</returns>
        public static bool HasTimePortion(this DateTime value)
        {
            return value.TimeOfDay.Ticks > 0;
        }
    }
}
