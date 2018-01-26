using System.Configuration;

namespace Tieptuyen.Components.Core.AssetStore.Configuration
{
    /// <summary>
    /// Represents a resource asset configuration element.
    /// </summary>
    public sealed class AssetStoreElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the alias for the asset store.
        /// </summary>
        [ConfigurationProperty("alias", IsRequired = true)]
        public string Alias
        {
            get { return this["alias"] as string; }
        }

        /// <summary>
        /// Gets the fully-qualified type used for the asset store.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"] as string; }
        }

        /// <summary>
        /// Gets any parameters required when creating the asset store.
        /// </summary>
        /// <remarks>Parameters are in the form of a semi-colon-separated string where each parameter is in the form of key=value;</remarks>
        [ConfigurationProperty("parameters", IsRequired = false)]
        public string Parameters
        {
            get { return this["parameters"] as string; }
        }
    }
}
