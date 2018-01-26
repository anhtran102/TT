using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Tieptuyen.Components.Core.Api.Util;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// A virtual asset store which is based on the local file-system.
    /// </summary>
    public sealed class FileSystemAssetStore : AssetStore
    {
        private string rootDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemAssetStore"/> class.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="rootDirectory">The physical root directory of the asset store's location on disk.</param>
        public FileSystemAssetStore(string alias, string rootDirectory)
            : base(alias)
        {
            this.rootDirectory = rootDirectory;
        }

        /// <inheritdoc/>
        public override bool FileExists(string virtualPath)
        {
            if (this.IsPathVirtual(virtualPath))
            {
                string physicalPath = this.GetPhysicalPath(virtualPath);
                return File.Exists(physicalPath);
            }
            else
                return this.Previous.FileExists(virtualPath);
        }

        /// <inheritdoc/>
        public override VirtualFile GetFile(string virtualPath)
        {
            if (this.IsPathVirtual(virtualPath))
            {
                if (this.FileExists(virtualPath))
                    return new FileSystemAssetStoreFile(virtualPath, this);
                else
                    throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "File '{0}' does not exist.", virtualPath));
            }
            else
                return this.Previous.GetFile(virtualPath);
        }

        /// <inheritdoc/>
        public override bool DirectoryExists(string virtualDir)
        {
            if (this.IsPathVirtual(virtualDir))
            {
                string physicalPath = this.GetPhysicalPath(virtualDir);
                return Directory.Exists(physicalPath);
            }
            else
                return this.Previous.DirectoryExists(virtualDir);
        }

        /// <inheritdoc/>
        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            if (this.IsPathVirtual(virtualDir))
            {
                if (this.DirectoryExists(virtualDir))
                    return new FileSystemAssetStoreDirectory(virtualDir, this);
                else
                    throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "Directory '{0}' does not exist.", virtualDir));
            }
            else
                return this.Previous.GetDirectory(virtualDir);
        }

        /// <summary>
        /// Gets the physical path from a virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>A <see cref="System.String"/> representing the physical path.</returns>
        internal string GetPhysicalPath(string virtualPath)
        {
            string appRelativePath = VirtualPathUtility.ToAppRelative(virtualPath);
            string relativePath = appRelativePath.CaseInsensitiveReplace(this.RootVirtualPath, string.Empty).Replace('/', '\\');
            return Path.Combine(this.rootDirectory, relativePath);
        }
    }
}
