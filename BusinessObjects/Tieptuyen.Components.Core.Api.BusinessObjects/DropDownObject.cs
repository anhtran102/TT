using System.Runtime.Serialization;
using Tieptuyen.Components.Core.Api.ObjectModel;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// The drop down object
    /// </summary>
    [DataContract]
    public class DropDownObject : INameOrderable
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
