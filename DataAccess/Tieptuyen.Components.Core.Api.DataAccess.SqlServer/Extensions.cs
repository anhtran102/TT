using System;
using System.Collections.Generic;
using Tieptuyen.Components.Core.Api.Util;

namespace Tieptuyen.Components.Core.Api.DataAccess.SqlServer
{
    /// <summary>
    /// Provides extension methods for use during data access operations.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Processes the output (inserts, updates and deletes) from a stored procedure.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="onRowInserted">The action to perform for each row inserted.</param>
        /// <param name="onRowUpdated">The action to perform for each row updated.</param>
        /// <param name="onRowDeleted">The action to perform for each row deleted.</param>
        /// <returns>The original output.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="output"/> is null.</exception>
        /// <remarks>Each object in the collection specified by the <paramref name="output"/> parameter, must have a property named <c>Action</c> of type <see cref="System.String"/>.
        /// The property can have a value of &quot;INSERT&quot;, &quot;UPDATE&quot; or &quot;DELETE&quot;</remarks>
        public static IEnumerable<dynamic> Process(this IEnumerable<dynamic> output, Action<dynamic> onRowInserted = null, Action<dynamic> onRowUpdated = null, Action<dynamic> onRowDeleted = null)
        {
            if (output != null)
            {
                output.Each(
                    o =>
                    {
                        string action = o.Action;
                        switch (action)
                        {
                            case "INSERT":
                                if (onRowInserted != null)
                                    onRowInserted(o);
                                break;
                            case "UPDATE":
                                if (onRowUpdated != null)
                                    onRowUpdated(o);
                                break;
                            case "DELETE":
                                if (onRowDeleted != null)
                                    onRowDeleted(o);
                                break;
                        }
                    });

                return output;
            }
            else
                throw new ArgumentNullException("output", "The output cannot be null.");
        }
    }
}
