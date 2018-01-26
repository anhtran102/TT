using System;
using Tieptuyen.Components.Core.Api.Configuration;
using Microsoft.Practices.Unity;

namespace Tieptuyen.Components.Core.Api.Services
{
    /// <summary>
    /// Provides functionality for working with dates and times.
    /// </summary>
    public sealed class DateService : IDateService
    {
        private dynamic configurationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateService"/> class.
        /// </summary>
        /// <param name="configurationManager">The configuration manager.</param>
        [InjectionConstructor]
        public DateService(IConfigurationManager configurationManager)
        {
            this.configurationManager = configurationManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateService"/> class.
        /// </summary>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <remarks>This is for unit tests only.</remarks>
        public DateService(dynamic configurationManager)
        {
            this.configurationManager = configurationManager;
        }

        /// <inheritdoc/>
        public DateTime GetCurrentDate()
        {
            return this.GetCurrentDateTime().Date;
        }

        /// <inheritdoc/>
        public DateTime GetCurrentDateTime()
        {
            if (this.configurationManager.SystemTimeZone == null)
            {
                return this.GetDateTimeFromTimeOffset(this.configurationManager.PriceNetOffset);
            }
            else
            {
                return this.GetDateTimeFromTimeZoneInfo(this.configurationManager.SystemTimeZone);
            }
        }

        /// <inheritdoc/>
        public DateTime GetCurrentUtcDateTime()
        {
            return DateTime.UtcNow;
        }

        private DateTime GetDateTimeFromTimeOffset(int timeOffset)
        {
            return DateTime.Now.AddMinutes(timeOffset); 
        }

        private DateTime GetDateTimeFromTimeZoneInfo(string timeZoneName)
        {
            DateTime timeUtc = DateTime.UtcNow;
            try
            {
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
                return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            }
            catch (TimeZoneNotFoundException)
            {
                this.GetDateTimeFromTimeOffset(this.configurationManager.PriceNetOffset);
            }
            catch (InvalidTimeZoneException)
            {
                this.GetDateTimeFromTimeOffset(this.configurationManager.PriceNetOffset);
            }

            return this.GetDateTimeFromTimeOffset(this.configurationManager.PriceNetOffset);
        }
    }
}
