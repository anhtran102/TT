using System;
using System.Globalization;
using System.Web;
using System.Web.Hosting;
using Tieptuyen.Components.Core.AssetStore.Configuration;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// The base class, from which all asset stores inherit.
    /// </summary>
    public abstract class AssetStore : VirtualPathProvider, IAssetStore
    {
        private static readonly string Root;

        static AssetStore()
        {
            AssetStoreConfiguration config = AssetStoreConfiguration.GetConfiguration();
            Root = config.Root;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetStore"/> class.
        /// </summary>
        /// <param name="alias">The alias for the asset store.</param>
        protected AssetStore(string alias)
        {
            this.Alias = alias;
        }        

        /// <summary>
        /// Gets the alias for this asset store.
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// Gets the root virtual path.
        /// </summary>
        protected string RootVirtualPath
        {
            get { return string.Format(CultureInfo.InvariantCulture, "~/{0}/{1}/", Root, this.Alias); }
        }

        /// <inheritdoc/>
        bool IAssetStore.FileExists(string path)
        {
            return this.FileExists(this.ToFullVirtualPath(path));
        }

        /// <inheritdoc/>
        bool IAssetStore.DirectoryExists(string path)
        {
            return this.DirectoryExists(this.ToFullVirtualPath(path));
        }

        /// <inheritdoc/>
        IAssetStoreFile IAssetStore.GetFile(string path)
        {
            return (IAssetStoreFile)this.GetFile(this.ToFullVirtualPath(path));
        }

        /// <inheritdoc/>
        IAssetStoreDirectory IAssetStore.GetDirectory(string path)
        {
            return (IAssetStoreDirectory)this.GetDirectory(this.ToFullVirtualPath(path));
        }        

        /// <summary>
        /// Determines whether the supplied path is a valid virtual path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if <paramref name="path"/> is a valid virtual path, otherwise <c>false</c>.</returns>
        protected virtual bool IsPathVirtual(string path)
        {
            string checkPath = VirtualPathUtility.ToAppRelative(path);
            return checkPath.StartsWith(this.RootVirtualPath, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Creates a full virtual path from a relative path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The full virtual path as a <see cref="String"/>.</returns>
        protected virtual string ToFullVirtualPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || path == "/")
                return this.RootVirtualPath;
            else
                return this.CombineVirtualPaths(this.RootVirtualPath, path);
        }
    }
}
