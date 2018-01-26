using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a request to assign a user to a profile.
    /// </summary>
    [DataContract]
    public class AssignUserToProfileRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the profile identifier.
        /// </summary>
        [DataMember(Name = "profileId")]
        public int ProfileID { get; set; }
    }
}
