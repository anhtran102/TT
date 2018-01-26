using System;

namespace Tieptuyen.Components.Core.Api.ObjectModel
{
    /// <summary>
    /// Represents the specification by which objects in a collection are to be ordered.
    /// </summary>
    /// <typeparam name="T">The type of the object being sorted.</typeparam>
    public sealed class OrderBy<T>
    {
        private Func<T, IComparable> orderFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBy{T}"/> class.
        /// </summary>
        /// <param name="orderFunc">The order function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="orderFunc"/> is <c>null</c>.</exception>
        internal OrderBy(Func<T, IComparable> orderFunc)
        {
            if (orderFunc != null)
                this.orderFunc = orderFunc;
            else
                throw new ArgumentNullException("orderFunc", "The order function cannot be null.");
        }

        /// <summary>
        /// Sorts the objects in ascending order.
        /// </summary>
        /// <returns>A comparison object for sorting the results in ascending order.</returns>
        public Comparison<T> Ascending()
        {
            return (x, y) => Compare(x, y, this.orderFunc);
        }

        /// <summary>
        /// Sorts the objects in ascending order.
        /// </summary>
        /// <returns>A comparison object for sorting the results in ascending order.</returns>
        public Comparison<T> Descending()
        {
            return (x, y) => Compare(x, y, this.orderFunc) * -1;
        }

        private static int Compare<T>(T x, T y, Func<T, IComparable> orderFunc)
        {
            return After.CheckForNulls(x, y, (a, b) => After.CheckForNulls(orderFunc(a), orderFunc(b), (c, d) => c.CompareTo(d)));
        }
    }
}
