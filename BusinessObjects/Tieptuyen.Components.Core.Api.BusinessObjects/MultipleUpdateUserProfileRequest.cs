using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents the multiple update user profile request
    /// </summary>
    [DataContract]
    public class MultipleUpdateUserProfileRequest
    {
        /// <summary>
        /// Gets or sets list of profile id
        /// </summary>
        [DataMember(Name = "profileIds")]
        public int[] ProfileIds { get; set; }

        /// <summary>
        /// Gets or sets profile
        /// </summary>
        [DataMember(Name = "profile")]
        public Profile Profile { get; set; }
    }
}
