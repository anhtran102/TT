using System.Collections.Generic;
using System.Configuration;

namespace Tieptuyen.Components.Core.AssetStore.Configuration
{
    /// <summary>
    /// A collection of asset store configuration elements.
    /// </summary>
    [ConfigurationCollection(typeof(AssetStoreElement))]
    public sealed class AssetStoreCollection : ConfigurationElementCollection
    {
        /// <inheritdoc/>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AssetStoreElement();
        }

        /// <inheritdoc/>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AssetStoreElement)element).Alias;
        }
    }
}
