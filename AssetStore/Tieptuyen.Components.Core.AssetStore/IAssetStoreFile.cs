using System.IO;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// Defines a file in the asset store.
    /// </summary>
    public interface IAssetStoreFile
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Opens a writable stream to this file.
        /// </summary>
        /// <returns>A writable <see cref="Stream"/> object.</returns>
        Stream Open();

        /// <summary>
        /// Opens a stream to this file.
        /// </summary>
        /// <param name="readOnly">if set to <c>true</c> a read-only stream will be returned.</param>
        /// <returns>A <see cref="Stream"/> object.</returns>
        Stream Open(bool readOnly);
    }
}
