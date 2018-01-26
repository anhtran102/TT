using System;
using System.Collections.Generic;

namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// Provides extension methods for dictionary objects.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Merges the specified source dictionary into the destination.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>The merged <paramref name="destination"/> dictionary.</returns>
        /// <exception cref="System.ArgumentException">Thrown if an item with the same key exists in both the source and destination dictionaries.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="source"/> or <paramref name="destination"/>is <c>null</c>.</exception>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> destination)
        {
            return source.Merge(destination, MergeConflictResolution.ThrowError);
        }

        /// <summary>
        /// Merges the specified source dictionary into the destination.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="conflictResolution">The conflict resolution option to use.</param>
        /// <returns>The merged <paramref name="destination"/> dictionary</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="conflictResolution"/> is set to <c>ThrowError</c> and an item with the same key exists in both the source and destination dictionaries.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="source"/> or <paramref name="destination"/>is <c>null</c>.</exception>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> destination, MergeConflictResolution conflictResolution)
        {
            if (source != null && destination != null)
            {
                foreach (TKey key in source.Keys)
                {
                    if (!destination.ContainsKey(key))
                        destination.Add(key, source[key]);
                    else
                    {
                        switch (conflictResolution)
                        {
                            case MergeConflictResolution.UseSource:
                                destination.Remove(key);
                                destination.Add(key, source[key]);
                                break;
                            case MergeConflictResolution.ThrowError:
                                string errorMessage = string.Format("An item with the key \"{0}\" exists in both the source and destination dictionaries", key);
                                throw new ArgumentException(errorMessage);
                        }
                    }
                }

                return destination;
            }
            else
            {
                if (source == null)
                    throw new ArgumentNullException("source", "The source cannot be null");
                else
                    throw new ArgumentNullException("destination", "The destination cannot be null");
            }
        }
    }
}
