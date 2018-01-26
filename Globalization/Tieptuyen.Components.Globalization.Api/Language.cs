using System.Runtime.Serialization;

namespace Tieptuyen.Components.Globalization.Api
{
    /// <summary>
    /// Represents a language supported by the system.
    /// </summary>
    [DataContract]
    public class Language
    {
        /// <summary>
        /// The default language.
        /// </summary>
        public static readonly string DefaultLanguage = "en-US";

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <remarks>
        /// The language ID is in the form of a culture specifier, for example:
        /// <list type="table">
        /// <listheader><term>ID</term><description>Description</description></listheader>
        /// <item><term>en-GB</term><description>English (British)</description></item>
        /// <item><term>en-US</term><description>English (United States)</description></item>
        /// <item><term>de-DE</term><description>Deutsch (Deutschland)</description></item>
        /// </list>
        /// </remarks>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}