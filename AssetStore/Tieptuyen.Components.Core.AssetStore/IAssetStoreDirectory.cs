using System.Collections.Generic;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// Defines a directory in the asset store.
    /// </summary>
    public interface IAssetStoreDirectory
    {
        /// <summary>
        /// Gets all sub-directories.
        /// </summary>
        IEnumerable<IAssetStoreDirectory> Directories { get; }

        /// <summary>
        /// Gets all files.
        /// </summary>
        IEnumerable<IAssetStoreFile> Files { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Determines whether a file exists.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns><c>true</c> if the file exists, otherwise <c>false</c>.</returns>
        bool FileExists(string fileName);

        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>An <see cref="IAssetStoreFile"/> object.</returns>
        IAssetStoreFile GetFile(string fileName);
        
        /// <summary>
        /// Creates a new file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The new <see cref="IAssetStoreFile"/> object.</returns>
        IAssetStoreFile CreateFile(string fileName);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        void DeleteFile(string fileName);

        /// <summary>
        /// Determines whether a directory exists.
        /// </summary>
        /// <param name="name">The name of the directory.</param>
        /// <returns><c>true</c> if the directory exists <c>otherwise</c> false.</returns>
        bool DirectoryExists(string name);

        /// <summary>
        /// Gets a directory.
        /// </summary>
        /// <param name="name">The name of the directory.</param>
        /// <returns>An <see cref="IAssetStoreDirectory"/> object.</returns>
        IAssetStoreDirectory GetDirectory(string name);

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="name">The name of the directory.</param>
        /// <returns>The new <see cref="IAssetStoreDirectory"/>.</returns>
        IAssetStoreDirectory CreateDirectory(string name);

        /// <summary>
        /// Deletes a directory.
        /// </summary>
        /// <param name="name">The name of the directory.</param>
        void DeleteDirectory(string name);
    }
}
