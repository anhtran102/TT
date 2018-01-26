using System.Configuration;

namespace Tieptuyen.Components.Core.Api.DataAccess.SqlServer
{
    /// <summary>
    /// A repository for performing data-access objects against the <c>kData</c> database.
    /// </summary>
    public abstract class KDataRepository : Repository
    {
        /// <inheritdoc/>
        public override string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Tieptuyen"].ConnectionString;
            }
        }
    }
}
