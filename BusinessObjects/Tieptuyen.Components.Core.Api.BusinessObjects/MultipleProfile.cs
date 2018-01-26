using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a multiple user profile.
    /// </summary>
    [DataContract]
    public class MultipleProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleProfile"/> class.
        /// </summary>
        public MultipleProfile()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for this profile.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

      
        /// <summary>
        /// Gets or sets Site for primary role.
        /// </summary>
        [DataMember(Name = "primaryRoleId")]
        public int PrimaryRoleID { get; set; }
    }
}
