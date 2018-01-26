using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a single user profile.
    /// </summary>
    [DataContract]
    public class Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
            this.Permissions = new Collection<WidgetPermission>();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this profile.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        [DataMember(Name = "permissions")]
        public Collection<WidgetPermission> Permissions { get; set; }       

        /// <summary>
        /// Gets or sets Site for primary role.
        /// </summary>
        [DataMember(Name = "primaryRoleId")]
        public int PrimaryRoleID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user can manage account locks.
        /// </summary>
        [DataMember(Name = "canManageAccountLocks")]
        public bool CanManageAccountLocks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user can manage account locks.
        /// </summary>
        [DataMember(Name = "membersOfUserProfiles")]
        public Collection<User> MembersOfUserProfiles { get; set; }
    }
}
