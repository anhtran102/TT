using System;
using System.Collections.Generic;

namespace Tieptuyen.Components.Core.Api.DataAccess
{
    /// <summary>
    /// Defines a grid reader.
    /// </summary>
    public interface IGridReader : IDisposable
    {
        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{T}"/>.</returns>
        IEnumerable<T> Read<T>(bool buffered = true);

        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{dynamic}"/>.</returns>
        IEnumerable<dynamic> Read(bool buffered = true);

        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="func">The mapper function.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{TReturn}"/>.</returns>
        IEnumerable<TReturn> Read<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> func, string splitOn = "id", bool buffered = true);

        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="func">The mapper function.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{TReturn}"/>.</returns>
        IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, string splitOn = "id", bool buffered = true);

        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TSixth">The type of the sixth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="func">The mapper function.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{TReturn}"/>.</returns>
        IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, string splitOn = "id", bool buffered = true);

        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TSixth">The type of the sixth object.</typeparam>
        /// <typeparam name="TSeventh">The type of the seventh object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="func">The mapper function.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{TReturn}"/>.</returns>
        IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, string splitOn = "id", bool buffered = true);

        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="func">The mapper function.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{TReturn}"/>.</returns>
        IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> func, string splitOn = "id", bool buffered = true);

        /// <summary>
        /// Reads from the grid reader.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="func">The mapper function.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="buffered">Set to <c>true</c> to perform a buffered read.</param>
        /// <returns>The results as an <see cref="IEnumerable{TReturn}"/>.</returns>
        IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> func, string splitOn = "id", bool buffered = true);
    }
}
