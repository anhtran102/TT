using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tieptuyen.Components.Core.Api.ObjectModel;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a single site for primary role.
    /// </summary>
    [DataContract]
    public class PrimaryRole : INameOrderable
    {
        /// <summary>
        /// Gets or sets the unique identifier for this site for primary role.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the translation id.
        /// </summary>
        [DataMember(Name = "translationId")]
        public string TranslationID { get; set; }
    }
}
