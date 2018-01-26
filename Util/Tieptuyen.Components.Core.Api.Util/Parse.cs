namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// A class for performing parsing operations.
    /// </summary>
    public static class Parse
    {
        /// <summary>
        /// Parses a string as the specified type.
        /// </summary>
        /// <typeparam name="T">The type to parse the string as.</typeparam>
        /// <param name="value">The string value.</param>
        /// <returns>A <see cref="ParseDescriptor{T}"/> object for performing the parse operation.</returns>
        public static ParseDescriptor<T> As<T>(string value)
            where T : struct
        {
            return new ParseDescriptor<T>(value);
        }
    }
}
