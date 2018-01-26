using System;

namespace Tieptuyen.Components.Core.Api.ObjectModel
{
    /// <summary>
    /// Helper class for ordering objects in a collection.
    /// </summary>
    public static class Order
    {
        /// <summary>
        /// Specifies the function by which objects are to be ordered.
        /// </summary>
        /// <typeparam name="T">The type of the object being ordered.</typeparam>
        /// <param name="orderFunc">The order function.</param>
        /// <returns>A specification for ordering the objects.</returns>
        public static OrderBy<T> By<T>(Func<T, IComparable> orderFunc)
        {
            return new OrderBy<T>(orderFunc);
        }
    }
}
