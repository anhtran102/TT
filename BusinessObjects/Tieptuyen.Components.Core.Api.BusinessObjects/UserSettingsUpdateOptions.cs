using System;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Specifies the options for updating user settings.
    /// </summary>
    [Flags]
    public enum UserSettingsUpdateOptions
    {
        /// <summary>
        /// Update basic settings.
        /// </summary>
        Basic = 1,

        /// <summary>
        /// Update the site metrics items.
        /// </summary>
        SiteMetricsItems = 8,

        /// <summary>
        /// Update the mixing desk items.
        /// </summary>
        MixingDeskItems = 16
    }
}
