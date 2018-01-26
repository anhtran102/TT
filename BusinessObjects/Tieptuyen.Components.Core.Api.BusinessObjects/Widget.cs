using System;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Represents a widget.
    /// </summary>
    [DataContract]
    public class Widget
    {
        /// <summary>
        /// Gets or sets the widget ID.
        /// </summary>
        [DataMember(Name = "id")]
        public int WidgetId { get; set; }

        /// <summary>
        /// Gets or sets the type of widget.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the title translation id.
        /// </summary>
        [DataMember(Name = "titleTxId")]
        public string TitleTxId { get; set; }

        /// <summary>
        /// Gets or sets the title default text.
        /// </summary>
        [DataMember(Name = "title")]
        public string TitleDefaultText { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember(Name = "descriptionTranslationId")]
        public string DescriptionTranslationId { get; set; }

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        [DataMember(Name = "obj")]
        public string Obj { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        [DataMember(Name = "order")]
        public byte Order { get; set; }       

        /// <summary>
        /// Gets or sets when the widget was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets when the widget was last updated.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the permission level.
        /// </summary>
        public PermissionLevel PermissionLevel { get; set; }

        /// <summary>
        /// Gets a value indicating whether the user has read-permissions on this widget.
        /// </summary>
        [DataMember(Name = "canRead")]
        public bool CanRead
        {
            get { return (this.PermissionLevel & PermissionLevel.Read) == PermissionLevel.Read; }
        }

        /// <summary>
        /// Gets a value indicating whether the user has write-permissions on this widget.
        /// </summary>
        [DataMember(Name = "canWrite")]
        public bool CanWrite
        {
            get { return (this.PermissionLevel & PermissionLevel.Write) == PermissionLevel.Write; }
        }
    }
}