using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Tieptuyen.Components.Core.Api.DataAccess
{
    /// <summary>
    /// A base class for creating unit of work objects.
    /// </summary>
    public abstract class UnitOfWork : IUnitOfWork
    {
        private bool disposed;

        /// <inheritdoc/>
        public virtual IDbTransaction Transaction { get; protected set; }

        /// <inheritdoc/>
        public virtual IDbConnection Connection { get; protected set; }

        /// <inheritdoc/>
        public virtual void Commit()
        {
            this.WithCheckForDisposal(this.Transaction.Commit);
        }

        /// <inheritdoc/>
        public virtual void Rollback()
        {
            this.WithCheckForDisposal(this.Transaction.Rollback);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }        

        /// <inheritdoc/>
        public virtual IEnumerable<dynamic> Query(string sql, dynamic parameters = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query(this.Connection, sql, parameters, this.Transaction, buffered, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<T> Query<T>(string sql, dynamic parameters = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<T>(this.Connection, sql, parameters, this.Transaction, buffered, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<T>> QueryAsAsync<T>(string sql, dynamic parameters = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<T>(this.Connection, sql, parameters, this.Transaction, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<TFirst, TSecond, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<TFirst, TSecond, TThird, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TReturn>> QueryAsAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, dynamic parameters = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this.Connection, sql, map, parameters, this.Transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <inheritdoc/>
        public virtual IGridReader QueryMultiple(string sql, dynamic parameters = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader dapperGridReader = SqlMapper.QueryMultiple(this.Connection, sql, parameters, this.Transaction, commandTimeout, commandType);
            return new GridReader(dapperGridReader);
        }

        /// <inheritdoc/>
        public virtual int Execute(string sql, dynamic parameters, int? commandTimeOut = null, CommandType? commandType = null)
        {
            return SqlMapper.Execute(this.Connection, sql, parameters, this.Transaction, commandTimeOut, commandType);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Transaction.Dispose();
                    this.Connection.Dispose();
                }

                this.disposed = true;
            }
        }

        private void WithCheckForDisposal(Action action)
        {
            if (!this.disposed)
                action();
            else
                throw new ObjectDisposedException(this.GetType().Name, "This unit of work has already been disposed.");
        }
    }
}
