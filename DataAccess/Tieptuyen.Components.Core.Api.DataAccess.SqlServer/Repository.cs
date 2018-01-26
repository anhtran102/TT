using System.Data.SqlClient;

namespace Tieptuyen.Components.Core.Api.DataAccess.SqlServer
{
    /// <inheritdoc/>
    public abstract class Repository : IRepository
    {       
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public abstract string ConnectionString { get; }

        /// <inheritdoc/>
        public IUnitOfWork BeginWork()
        {
            SqlConnection sqlConnection = new SqlConnection(this.ConnectionString);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            return new SqlUnitOfWork(sqlTransaction);
        }
    }
}
