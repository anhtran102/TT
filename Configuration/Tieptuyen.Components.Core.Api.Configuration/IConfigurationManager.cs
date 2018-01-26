using System.Collections.Generic;
using System.Dynamic;

namespace Tieptuyen.Components.Core.Api.Configuration
{
    /// <summary>
    /// A manager for handling configuration values in the system.
    /// </summary>
    public interface IConfigurationManager : IDynamicMetaObjectProvider
    {
        /// <summary>
        /// Gets all configuration values.
        /// </summary>
        /// <returns>A dictionary containing all the current configuration values.</returns>
        IDictionary<string, object> GetAllValues();

        /// <summary>
        /// Gets a configuration value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The configuration value stored against the specified key.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="key"/> is either <c>null</c> or empty.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if the value specified by <paramref name="key"/> does not exist.</exception>
        object GetValue(string key);

        /// <summary>
        /// Gets a subset of values.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns>A dictionary containing the configuration values.</returns>
        IDictionary<string, object> GetValues(string[] keys);

        /// <summary>
        /// Sets a configuration value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        void SetValue(string key, object value, ValueType type);
    }
}
