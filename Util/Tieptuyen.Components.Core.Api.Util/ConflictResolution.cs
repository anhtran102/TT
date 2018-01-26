namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// Options for resolving conflicts when merging dictionaries
    /// </summary>
    public enum MergeConflictResolution
    {
        /// <summary>
        /// Specifies an error should be thrown.
        /// </summary>
        ThrowError,

        /// <summary>
        /// Specifies that the source value should be used.
        /// </summary>
        UseSource,

        /// <summary>
        /// Specifies that the destination value should be used.
        /// </summary>
        UseDestination
    }
}
