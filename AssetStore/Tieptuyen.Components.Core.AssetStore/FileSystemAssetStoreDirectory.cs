using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// A asset directory which is based on the local file system.
    /// </summary>
    public sealed class FileSystemAssetStoreDirectory : VirtualDirectory, IAssetStoreDirectory
    {
        private FileSystemAssetStore assetStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemAssetStoreDirectory"/> class.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="assetStore">The asset store.</param>
        public FileSystemAssetStoreDirectory(string virtualPath, FileSystemAssetStore assetStore)
            : base(virtualPath)
        {
            this.assetStore = assetStore;
        }

        /// <inheritdoc/>
        public override IEnumerable Children
        {
            get { return this.GetAllSubDirectories().Cast<object>().Union(this.GetAllFiles().Cast<object>()); }
        }

        /// <inheritdoc/>
        IEnumerable<IAssetStoreDirectory> IAssetStoreDirectory.Directories
        {
            get { return this.Directories.Cast<IAssetStoreDirectory>(); }
        }

        /// <inheritdoc/>
        IEnumerable<IAssetStoreFile> IAssetStoreDirectory.Files
        {
            get { return this.Files.Cast<IAssetStoreFile>(); }
        }

        /// <inheritdoc/>
        public override IEnumerable Directories
        {
            get { return this.GetAllSubDirectories(); }
        }

        /// <inheritdoc/>
        public override IEnumerable Files
        {
            get { return this.GetAllFiles(); }
        }

        /// <inheritdoc/>
        string IAssetStoreDirectory.Name
        {
            get { return VirtualPathUtility.GetFileName(this.VirtualPath); }
        }

        /// <inheritdoc/>
        bool IAssetStoreDirectory.FileExists(string fileName)
        {
            return this.assetStore.FileExists(this.assetStore.CombineVirtualPaths(this.VirtualPath, fileName));
        }

        /// <inheritdoc/>
        IAssetStoreFile IAssetStoreDirectory.GetFile(string fileName)
        {
            return (IAssetStoreFile)this.assetStore.GetFile(this.assetStore.CombineVirtualPaths(this.VirtualPath, fileName));
        }

        /// <inheritdoc/>
        IAssetStoreFile IAssetStoreDirectory.CreateFile(string fileName)
        {
            string newVirtualPath = this.assetStore.CombineVirtualPaths(this.VirtualPath, fileName);
            if (!this.assetStore.FileExists(newVirtualPath))
                return new FileSystemAssetStoreFile(newVirtualPath, this.assetStore);
            else
                throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "The file '{0}' already exists.", fileName));
        }

        /// <inheritdoc/>
        void IAssetStoreDirectory.DeleteFile(string fileName)
        {
            string virtualPath = this.assetStore.CombineVirtualPaths(this.VirtualPath, fileName);
            if (this.assetStore.FileExists(virtualPath))
            {
                string physicalPath = this.assetStore.GetPhysicalPath(virtualPath);
                File.Delete(physicalPath);
            }
        }

        /// <inheritdoc/>
        bool IAssetStoreDirectory.DirectoryExists(string name)
        {
            return this.assetStore.DirectoryExists(this.assetStore.CombineVirtualPaths(this.VirtualPath, name));
        }

        /// <inheritdoc/>
        IAssetStoreDirectory IAssetStoreDirectory.GetDirectory(string name)
        {
            return (IAssetStoreDirectory)this.assetStore.GetDirectory(this.assetStore.CombineVirtualPaths(this.VirtualPath, name));
        }

        /// <inheritdoc/>
        IAssetStoreDirectory IAssetStoreDirectory.CreateDirectory(string name)
        {
            string newVirtualPath = this.assetStore.CombineVirtualPaths(this.VirtualPath, name);
            if (!this.assetStore.DirectoryExists(newVirtualPath))
            {
                string newPhysicalPath = this.assetStore.GetPhysicalPath(newVirtualPath);
                Directory.CreateDirectory(newPhysicalPath);
                return new FileSystemAssetStoreDirectory(newVirtualPath, this.assetStore);
            }
            else
                throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "The directory '{0}' already exists.", name));
        }

        /// <inheritdoc/>
        void IAssetStoreDirectory.DeleteDirectory(string name)
        {
            string virtualPath = this.assetStore.CombineVirtualPaths(this.VirtualPath, name);
            if (this.assetStore.DirectoryExists(virtualPath))
            {
                string physicalPath = this.assetStore.GetPhysicalPath(virtualPath);
                Directory.Delete(physicalPath, true);
            }
        }        

        private IEnumerable<VirtualFile> GetAllFiles()
        {
            IList<VirtualFile> virtualFiles = new List<VirtualFile>();
            string physicalPath = this.assetStore.GetPhysicalPath(this.VirtualPath);
            DirectoryInfo dir = new DirectoryInfo(physicalPath);
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                string virtualFilePath = this.assetStore.CombineVirtualPaths(this.VirtualPath, files[i].Name);
                VirtualFile virtualFile = new FileSystemAssetStoreFile(virtualFilePath, this.assetStore);
                virtualFiles.Add(virtualFile);
            }

            return virtualFiles;
        }

        private IEnumerable<VirtualDirectory> GetAllSubDirectories()
        {
            IList<VirtualDirectory> virtualDirs = new List<VirtualDirectory>();
            string physicalPath = this.assetStore.GetPhysicalPath(this.VirtualPath);
            DirectoryInfo dir = new DirectoryInfo(physicalPath);
            DirectoryInfo[] subDirs = dir.GetDirectories();
            for (int i = 0; i < subDirs.Length; i++)
            {
                string virtualDirPath = this.assetStore.CombineVirtualPaths(this.VirtualPath, subDirs[i].Name);
                VirtualDirectory virtualDir = new FileSystemAssetStoreDirectory(virtualDirPath, this.assetStore);
                virtualDirs.Add(virtualDir);
            }

            return virtualDirs;
        }        
    }
}
