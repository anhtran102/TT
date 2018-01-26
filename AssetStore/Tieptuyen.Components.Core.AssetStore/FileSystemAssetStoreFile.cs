using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// A asset file which based on the local file-system.
    /// </summary>
    public sealed class FileSystemAssetStoreFile : VirtualFile, IAssetStoreFile
    {
        private FileSystemAssetStore assetStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemAssetStoreFile"/> class.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="assetStore">The asset store.</param>
        public FileSystemAssetStoreFile(string virtualPath, FileSystemAssetStore assetStore)
            : base(virtualPath)
        {
            this.assetStore = assetStore;
        }

        /// <inheritdoc/>
        string IAssetStoreFile.FileName
        {
            get { return VirtualPathUtility.GetFileName(this.VirtualPath); }
        }

        /// <inheritdoc/>
        Stream IAssetStoreFile.Open()
        {
            return ((IAssetStoreFile)this).Open(false);
        }

        /// <inheritdoc/>
        Stream IAssetStoreFile.Open(bool readOnly)
        {
            string physicalPath = this.assetStore.GetPhysicalPath(this.VirtualPath);
            return new FileStream(physicalPath, FileMode.OpenOrCreate, readOnly ? FileAccess.Read : FileAccess.ReadWrite, readOnly ? FileShare.Read : FileShare.None);
        }

        /// <inheritdoc/>
        public override Stream Open()
        {
            string physicalPath = this.assetStore.GetPhysicalPath(this.VirtualPath);
            return new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
