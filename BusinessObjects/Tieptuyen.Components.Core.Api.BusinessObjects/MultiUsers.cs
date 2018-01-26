using System;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a pRE user.
    /// </summary>
    [DataContract]
    public class MultiUsers
    {
        /// <summary>
        /// Gets or sets the list id of user
        /// </summary>
        [DataMember(Name = "ids")]
        public int[] Ids { get; set; }

        /// <summary>
        /// Gets or sets the list of users
        /// </summary>
        [DataMember(Name = "users")]
        public User[] Users { get; set; }

        /// <summary>
        /// Gets or sets the profile ID.
        /// </summary>
        [DataMember(Name = "profileId")]
        public int? ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the preferred language.
        /// </summary>
        [DataMember(Name = "preferredLanguage")]
        public string PreferredLanguage { get; set; }

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
    }
}