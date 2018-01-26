using System;
using System.Collections.Generic;
using Dapper;

namespace Tieptuyen.Components.Core.Api.DataAccess
{
    /// <summary>
    /// A data grid reader.
    /// </summary>
    public sealed class GridReader : IGridReader
    {
        private SqlMapper.GridReader dapperGridReader;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridReader"/> class.
        /// </summary>
        /// <param name="dapperGridReader">The dapper grid reader.</param>
        public GridReader(SqlMapper.GridReader dapperGridReader)
        {
            this.dapperGridReader = dapperGridReader;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GridReader"/> class.
        /// </summary>
        ~GridReader()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public IEnumerable<T> Read<T>(bool buffered = true)
        {
            if (!this.disposed)
                return this.dapperGridReader.Read<T>(buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public IEnumerable<dynamic> Read(bool buffered = true)
        {
            if (!this.disposed)
                return this.dapperGridReader.Read(buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public IEnumerable<TReturn> Read<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            if (!this.disposed)
                return this.dapperGridReader.Read<TFirst, TSecond, TReturn>(func, splitOn, buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            if (!this.disposed)
                return this.Read<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(func, splitOn, buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            if (!this.disposed)
                return this.Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(func, splitOn, buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            if (!this.disposed)
                return this.Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(func, splitOn, buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            if (!this.disposed)
                return this.Read<TFirst, TSecond, TThird, TFourth, TReturn>(func, splitOn, buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            if (!this.disposed)
                return this.Read<TFirst, TSecond, TThird, TReturn>(func, splitOn, buffered);
            else
                throw new ObjectDisposedException(this.GetType().Name, "This grid reader has been disposed.");
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    this.dapperGridReader.Dispose();
                this.disposed = true;
            }
        }
    }
}
