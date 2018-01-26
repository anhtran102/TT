using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents the pRE-specific settings applied to a user.
    /// </summary>
    [DataContract]
    public class UserSettings : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
        {         
        }                

        /// <summary>
        /// Gets or sets a value indicating whether this user can manage account locks.
        /// </summary>
        [DataMember(Name = "canManageAccountLocks")]
        public bool CanManageAccountLocks { get; set; }       

        /// <summary>
        /// Gets or sets the widgets the user is allowed to access.
        /// </summary>
        [DataMember(Name = "permissions")]
        public Collection<Widget> Widgets { get; set; }                    
    }
}
