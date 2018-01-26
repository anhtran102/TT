namespace Tieptuyen.Components.Core.Api.Configuration
{
    /// <summary>
    /// Represents the data type of a configuration value.
    /// </summary>
    public enum ValueType
    {
        /// <summary>
        /// Specifies a boolean value.
        /// </summary>
        Boolean,

        /// <summary>
        /// Specifies a integer value.
        /// </summary>
        Integer,

        /// <summary>
        /// Specifies a float value.
        /// </summary>
        Float,

        /// <summary>
        /// Specifies a date value.
        /// </summary>
        Date,

        /// <summary>
        /// Specifies a date/time value.
        /// </summary>
        DateTime,

        /// <summary>
        /// Specifies a string value.
        /// </summary>
        String,

        /// <summary>
        /// Specifies an XML value.
        /// </summary>
        Xml
    }
}
