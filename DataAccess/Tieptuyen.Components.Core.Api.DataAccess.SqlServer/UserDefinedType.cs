using System.Collections.Generic;
using System.Linq;

namespace Tieptuyen.Components.Core.Api.DataAccess.SqlServer
{
    /// <summary>
    /// Provides extensions methods for working with SQL-Server user-defined types.
    /// </summary>
    public static class UserDefinedType
    {
        /// <summary>
        /// Converts a collection of integer IDs to an array of objects for use with the <c>cmn.Entity</c> type.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>An array of entity objects.</returns>
        public static object[] AsEntities(this IEnumerable<int> ids)
        {
            return ids.Select(x => new { EntityID = x }).ToArray();
        }
    }
}
