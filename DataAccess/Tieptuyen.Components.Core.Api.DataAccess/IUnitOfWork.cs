using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Tieptuyen.Components.Core.Api.DataAccess
{
    /// <summary>
    /// Represents an atomic unit of work to perform data access functionality.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the transaction.
        /// </summary>
        IDbTransaction Transaction { get; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Commits the unit of work to the repository.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks the unit of work.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IEnumerable{dynamic}"/> object.</returns>
        IEnumerable<dynamic> Query(string sql, dynamic parameters = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> object.</returns>
        IEnumerable<T> Query<T>(string sql, dynamic parameters = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IEnumerable{TReturn}"/> object.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IEnumerable{TReturn}" /> object.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IEnumerable{TReturn}" /> object.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IEnumerable{TReturn}" /> object.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TSixth">The type of the sixth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>
        /// An <see cref="IEnumerable{TReturn}" /> object.
        /// </returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TSixth">The type of the sixth object.</typeparam>
        /// <typeparam name="TSeventh">The type of the seventh object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IEnumerable{TReturn}" /> object.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Asynchronously runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{T}}"/> object.</returns>
        Task<IEnumerable<T>> QueryAsAsync<T>(string sql, dynamic parameters = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Asynchronously runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A <see cref="Task{IEnumerable{TReturn}}"/> object.</returns>
        Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Asynchronously runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A <see cref="Task{IEnumerable{TReturn}}" /> object.</returns>
        Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Asynchronously runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A <see cref="Task{IEnumerable{TReturn}}" /> object.</returns>
        Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Asynchronously runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A <see cref="Task{IEnumerable{TReturn}}" /> object.</returns>
        Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Asynchronously runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TSixth">The type of the sixth object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A <see cref="Task{IEnumerable{TReturn}}" /> object.</returns>
        Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Asynchronously runs a query as part of the specified unit of work.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first object.</typeparam>
        /// <typeparam name="TSecond">The type of the second object.</typeparam>
        /// <typeparam name="TThird">The type of the third object.</typeparam>
        /// <typeparam name="TFourth">The type of the fourth object.</typeparam>
        /// <typeparam name="TFifth">The type of the fifth object.</typeparam>
        /// <typeparam name="TSixth">The type of the sixth object.</typeparam>
        /// <typeparam name="TSeventh">The type of the seventh object.</typeparam>
        /// <typeparam name="TReturn">The type of the return object.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="buffered">Set to <c>true</c> to buffer the query.</param>
        /// <param name="splitOn">The column to split on.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A <see cref="Task{IEnumerable{TReturn}}" /> object.</returns>
        Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Runs a query as part of the specified unit of work, which returns multiple result sets.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An <see cref="IGridReader"/> object.</returns>
        IGridReader QueryMultiple(string sql, dynamic parameters = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes a SQL statement as part of the specified unit of work.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="commandTimeOut">The command time out.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>The number of rows affected, as an <see cref="Int32"/>.</returns>
        int Execute(string sql, dynamic parameters, int? commandTimeOut = null, CommandType? commandType = null);
    }
}
