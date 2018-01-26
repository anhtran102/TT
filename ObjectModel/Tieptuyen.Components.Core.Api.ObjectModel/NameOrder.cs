using System;

namespace Tieptuyen.Components.Core.Api.ObjectModel
{
    /// <summary>
    /// Defines how objects should be sorted according to their name.
    /// </summary>
    public static class NameOrder
    {
        /// <summary>
        /// Gets the comparison to sort objects in ascending order.
        /// </summary>
        public static Comparison<INameOrderable> Ascending
        {
            get { return Order.By<INameOrderable>(x => x.Name).Ascending(); }
        }

        /// <summary>
        /// Gets the comparison to sort objects in ascending order.
        /// </summary>
        public static Comparison<INameOrderable> Descending
        {
            get { return Order.By<INameOrderable>(x => x.Name).Descending(); }
        }
    }
}
