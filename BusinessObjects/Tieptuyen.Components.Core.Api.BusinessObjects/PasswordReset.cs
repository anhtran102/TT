using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a request to reset a user's password.
    /// </summary>
    [DataContract]
    public class PasswordReset
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the existing password.
        /// </summary>
        [DataMember(Name = "existingPassword")]
        public string ExistingPassword { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation.
        /// </summary>
        [DataMember(Name = "passwordConfirmation")]
        public string PasswordConfirmation { get; set; }

        /// <summary>
        /// Gets or sets the encrypted password.
        /// </summary>
        public string EncryptedPassword { get; set; }
    }
}
