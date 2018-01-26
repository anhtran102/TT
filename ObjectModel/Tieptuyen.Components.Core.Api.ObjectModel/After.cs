using System;

namespace Tieptuyen.Components.Core.Api.ObjectModel
{
    /// <summary>
    /// Class providing functionality <b>after</b> which the comparison will be performed.
    /// </summary>
    internal static class After
    {
        /// <summary>
        /// Checks for nulls.
        /// </summary>
        /// <typeparam name="T">The types for the comparison.</typeparam>
        /// <param name="x">Value x.</param>
        /// <param name="y">Value y.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The result of the comparison as an <see cref="Int32"/>.</returns>
        public static int CheckForNulls<T>(T x, T y, Func<T, T, int> comparison)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (y == null)
                    return 1;
                else
                    return comparison(x, y);
            }
        }
    }
}
