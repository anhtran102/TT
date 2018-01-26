using System;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a single permission on a widget.
    /// </summary>
    [Flags]
    public enum PermissionLevel : byte
    {
        /// <summary>
        /// Specifies that the user has no permissions on the widget.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies that the user has read permissions on the widget.
        /// </summary>
        Read = 1,

        /// <summary>
        /// Specifies that the user has write permissions on the widget.
        /// </summary>
        Write = 2
    }
}
