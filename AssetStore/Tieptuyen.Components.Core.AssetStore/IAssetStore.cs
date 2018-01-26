namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// Defines a virtual asset store.
    /// </summary>
    public interface IAssetStore
    {
        /// <summary>
        /// Gets the alias for this asset store.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Determines whether a file exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if the file exists, otherwise <c>false</c>.</returns>
        bool FileExists(string path);

        /// <summary>
        /// Determines whether a directory exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if the directory exists, otherwise <c>false</c>.</returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// Gets the specified file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>An <see cref="IAssetStoreFile"/> object.</returns>
        IAssetStoreFile GetFile(string path);

        /// <summary>
        /// Gets the the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>An <see cref="IAssetStoreDirectory"/> object.</returns>
        IAssetStoreDirectory GetDirectory(string path);
    }
}
