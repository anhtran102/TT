using System;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// An exception which may be thrown when an error occurs with the asset store system.
    /// </summary>
    [Serializable]
    public class AssetStoreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetStoreException"/> class.
        /// </summary>
        public AssetStoreException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetStoreException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AssetStoreException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetStoreException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AssetStoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetStoreException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected AssetStoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
