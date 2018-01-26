using System;
using System.Data;
using System.Data.SqlClient;

namespace Tieptuyen.Components.Core.Api.DataAccess.SqlServer
{
    /// <inheritdoc/>
    public sealed class SqlUnitOfWork : UnitOfWork
    {    
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlUnitOfWork"/> class.
        /// </summary>
        /// <param name="transaction">The SQL transaction.</param>
        public SqlUnitOfWork(SqlTransaction transaction)
            : base()
        {
            this.Transaction = transaction;
            this.Connection = transaction.Connection;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlUnitOfWork"/> class.
        /// </summary>
        ~SqlUnitOfWork()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets or sets the SQL-Server connection.
        /// </summary>
        public new SqlConnection Connection
        {
            get { return (SqlConnection)base.Connection; }
            set { base.Connection = value; }
        }

        /// <summary>
        /// Gets or sets the SQL-Server transaction.
        /// </summary>
        public new SqlTransaction Transaction
        {
            get { return (SqlTransaction)base.Transaction; }
            set { base.Transaction = value; }
        }
    }
}
