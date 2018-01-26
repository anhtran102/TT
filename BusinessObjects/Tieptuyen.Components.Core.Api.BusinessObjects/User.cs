using System;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a pRE user.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the profile ID.
        /// </summary>
        [DataMember(Name = "profileId")]
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        [DataMember(Name = "profileName")]
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the primary role id.
        /// </summary>
        [DataMember(Name = "primaryRoleId")]
        public int PrimaryRoleId { get; set; }
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

        /// <summary>
        /// Gets or sets the preferred language.
        /// </summary>
        [DataMember(Name = "preferredLanguage")]
        public string PreferredLanguage { get; set; }       

        /// <summary>
        /// Gets or sets when the user was created.
        /// </summary>
        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets when the user was last updated.
        /// </summary>
        [DataMember(Name = "lastUpdated")]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user selected.
        /// </summary>
        [DataMember(Name = "isSelected")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is locked.
        /// </summary>
        [DataMember(Name = "locked")]
        public bool Locked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how many times you enter wrong password
        /// </summary>
        [DataMember(Name = "passwordAttemptCount")]
        public int PasswordAttemptCount { get; set; }
    }
}