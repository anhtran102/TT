using System.Dynamic;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// Defines a asset store manager.
    /// </summary>
    public interface IAssetStoreManager : IDynamicMetaObjectProvider
    {
        /// <summary>
        /// Registers a asset store.
        /// </summary>
        /// <param name="assetStore">The asset store.</param>
        /// <exception cref="Tieptuyen.Components.Core.ResourceStore.ResourceStoreException">Thrown if an existing resource store has been registered with the same alias.</exception>
        void RegisterAssetStore(IAssetStore assetStore);

        /// <summary>
        /// Loads the asset stores from configuration (web.config or app.config).
        /// </summary>
        void LoadFromConfiguration();

        /// <summary>
        /// Gets a asset store.
        /// </summary>
        /// <param name="alias">The alias of the asset store.</param>
        /// <returns>An <see cref="IAssetStore"/> object.</returns>
        IAssetStore GetResourceStore(string alias);
    }
}
