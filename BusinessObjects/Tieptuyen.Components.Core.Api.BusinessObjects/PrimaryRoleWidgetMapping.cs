using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// The primary role widget mapping.
    /// </summary>
    [DataContract]
    public class PrimaryRoleWidgetMapping
    {
        /// <summary>
        /// Gets or sets the primary role widget mapping id.
        /// </summary>
        [DataMember(Name = "primaryRoleWidgetMappingId")]
        public int PrimaryRoleWidgetMappingId { get; set; }

        /// <summary>
        /// Gets or sets the primary role id.
        /// </summary>
        [DataMember(Name = "primaryRoleId")]
        public int PrimaryRoleId { get; set; }

        /// <summary>
        /// Gets or sets the widget id.
        /// </summary>
        [DataMember(Name = "widgetId")]
        public int WidgetId { get; set; }
    }
}
