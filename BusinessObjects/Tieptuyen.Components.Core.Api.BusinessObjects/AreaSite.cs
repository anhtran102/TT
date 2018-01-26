using System;
using System.Runtime.Serialization;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// Class to get identifiers of sites in an area what kind of site they are
    /// </summary>
    [DataContract]
    public class AreaSite
    {
        /// <summary>
        /// Gets or sets the SiteId
        /// </summary>
        [DataMember(Name = "siteId")]
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is an own site
        /// </summary>
        [DataMember(Name = "isOwnSite")]
        public bool IsOwnSite { get; set; }

        /// <summary>
        /// Gets or sets the OwnSiteId
        /// </summary>
        [DataMember(Name = "ownSiteId")]
        public int OwnSiteId { get; set; }

        /// <summary>
        /// Gets or sets the CompetitorSiteId
        /// </summary>
        [DataMember(Name = "competitorSiteId")]
        public int CompetitorSiteId { get; set; }

        /// <summary>
        /// Gets or sets the PlanningSiteId
        /// </summary>
        [DataMember(Name = "planningSiteId")]
        public int PlanningSiteId { get; set; }
    }
}
