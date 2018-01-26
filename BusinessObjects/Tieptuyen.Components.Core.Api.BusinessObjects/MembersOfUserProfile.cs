using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represent the members of user profile
    /// </summary>
    [DataContract]
    public class MembersOfUserProfile
    {
        /// <summary>
        /// Gets or sets users have been assigned.
        /// </summary>
        [DataMember(Name = "usersAssigned")]
        public IList<User> UsersAssigned { get; set; }

        /// <summary>
        /// Gets or sets users have been unassigned.
        /// </summary>
        [DataMember(Name = "usersUnAssigned")]
        public IList<User> UsersUnAssigned { get; set; }
    }
}
