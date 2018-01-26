using System.Configuration;

namespace Tieptuyen.Components.Core.AssetStore.Configuration
{
    /// <summary>
    /// The configuration for the asset stores.
    /// </summary>
    public sealed class AssetStoreConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets the root.
        /// </summary>
        [ConfigurationProperty("root", IsRequired = false, DefaultValue = "Assets")]
        public string Root
        {
            get { return this["root"] as string; }
        }

        /// <summary>
        /// Gets all the current stores.
        /// </summary>
        [ConfigurationProperty("stores")]
        public AssetStoreCollection Stores
        {
            get { return this["stores"] as AssetStoreCollection; }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns>A <see cref="AssetStoreConfiguration"/> object representing the current configuration.</returns>
        public static AssetStoreConfiguration GetConfiguration()
        {
            var config = ConfigurationManager.GetSection("assetStore") as AssetStoreConfiguration;
            return config ?? new AssetStoreConfiguration();
        }
    }
}
