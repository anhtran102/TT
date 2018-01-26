using System;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents the options to use when updating profiles in the database.
    /// </summary>
    [Flags]
    public enum ProfileUpdateOptions
    {
        /// <summary>
        /// Specifies no options
        /// </summary>
        None = 0,

        /// <summary>
        /// Updates the permissions assigned to the profile
        /// </summary>
        UpdatePermissions = 1
    }
}
