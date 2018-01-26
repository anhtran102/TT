using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// Provides extension methods to classes which implement the <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class Enumerable
    {
        /// <summary>
        /// Creates a collection from an arbitrary collection of objects.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="objs">The objects.</param>
        /// <returns>A <c>Collection</c> object.</returns>
        public static Collection<T> ToCollection<T>(this IEnumerable<T> objs)
        {
            if (objs != null)
            {
                Collection<T> collection = new Collection<T>();
                foreach (T obj in objs)
                    collection.Add(obj);
                return collection;
            }
            else
                throw new ArgumentNullException("objs", "The object collection cannot be null");
        }

        /// <summary>
        /// Preforms an action for each object in a collection.
        /// </summary>
        /// <typeparam name="T">The type of object in the collection.</typeparam>
        /// <param name="objs">The objects in the collection.</param>
        /// <param name="action">The action to perform.</param>
        /// <exception cref="System.ArgumentNullException">action;The action cannot be null.</exception>
        public static void Each<T>(this IEnumerable<T> objs, Action<T> action)
        {
            if (objs != null)
            {
                if (action != null)
                {
                    foreach (T obj in objs)
                        action(obj);
                }
                else
                    throw new ArgumentNullException("action", "The action cannot be null.");
            }
        }

        /// <summary>
        /// Returns distinct elements from a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="predicate">The predicate by which it is determined that two items are equal.</param>
        /// <returns> A list of distinct items.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="predicate"/> is <c>null</c>.</exception>
        /// <remarks>This method returns <c>null</c> if <paramref name="source"/> is <c>null</c>.</remarks>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> predicate)
        {
            if (predicate != null)
            {
                if (source != null)
                {
                    List<TSource> distinctValues = new List<TSource>();
                    foreach (TSource value in source)
                    {
                        if (distinctValues.Count == 0)
                            distinctValues.Add(value);
                        else
                        {
                            bool hasMatch = false;
                            foreach (TSource distictValue in distinctValues)
                            {
                                if (predicate(value, distictValue))
                                {
                                    hasMatch = true;
                                    break;
                                }
                            }

                            if (!hasMatch)
                                distinctValues.Add(value);
                        }
                    }

                    return distinctValues;
                }
                else
                    return null;
            }
            else
                throw new ArgumentNullException("predicate", "A predicate must be specified.");
        }

        /// <summary>
        /// Creates a dictionary from a collection of key/value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="keyValuePairs">The key/value pairs.</param>
        /// <returns>A new dictionary populated with the pairs of keys and values.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="keyValuePairs"/> is <c>null</c>.</exception>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            if (keyValuePairs != null)
            {
                Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
                keyValuePairs.Each(x => dictionary.Add(x.Key, x.Value));
                return dictionary;
            }
            else
                throw new ArgumentNullException("keyValuePairs", "The collection of key/value pairs cannot be null.");
        }
    }
}
