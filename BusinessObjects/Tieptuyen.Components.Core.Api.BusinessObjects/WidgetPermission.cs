using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the permission level on a widget.
    /// </summary>
    [SuppressMessage("Gendarme.Rules.Maintainability", "AvoidLackOfCohesionOfMethodsRule", Justification = "The MinimumMethodCount is not applicable to a data contract")]
    [DataContract]
    public class WidgetPermission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetPermission"/> class.
        /// </summary>
        public WidgetPermission()
        {           
        }

        /// <summary>
        /// Gets or sets the profile ID.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the widget ID.
        /// </summary>
        [DataMember(Name = "id")]
        public int WidgetId { get; set; }

        /// <summary>
        /// Gets or sets the title default text.
        /// </summary>
        [DataMember(Name = "title")]
        public string TitleDefaultText { get; set; }

        /// <summary>
        /// Gets or sets the translation code for the widget.
        /// </summary>
        public string TitleTxID { get; set; }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        [DataMember(Name = "obj")]
        public string Obj { get; set; }

        /// <summary>
        /// Gets or sets the permission level.
        /// </summary>
        public PermissionLevel PermissionLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to enable this permission.
        /// </summary>
        [DataMember(Name = "enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets IsChecked
        /// </summary>
        [DataMember(Name = "checked")]
        public bool? IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the widget view restriction.
        /// </summary>
        [DataMember(Name = "widgetViewRestriction")]
        public int WidgetViewRestriction { get; set; }

       /// <summary>
        /// Gets or sets a value indicating whether the profile has read permissions on the widget.
        /// </summary>
        [DataMember(Name = "canRead")]
        public bool CanRead
        {
            get
            {
                return (this.PermissionLevel & PermissionLevel.Read) == PermissionLevel.Read;
            }

            set
            {
                if (this.PermissionChanged(value, PermissionLevel.Read))
                    this.PermissionLevel = this.PermissionLevel ^ PermissionLevel.Read;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the profile has write permissions on the widget.
        /// </summary>
        [DataMember(Name = "canWrite")]
        public bool CanWrite
        {
            get
            {
                return (this.PermissionLevel & PermissionLevel.Write) == PermissionLevel.Write;
            }

            set
            {
                if (this.PermissionChanged(value, PermissionLevel.Write))
                    this.PermissionLevel = this.PermissionLevel ^ PermissionLevel.Write;
            }
        }

        private bool PermissionChanged(bool value, PermissionLevel permission)
        {
            return (value && (this.PermissionLevel & permission) != permission) || (!value && (this.PermissionLevel & permission) == permission);
        }
    }
}