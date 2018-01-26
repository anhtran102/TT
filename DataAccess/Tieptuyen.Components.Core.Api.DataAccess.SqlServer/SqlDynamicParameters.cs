using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dapper;

namespace Tieptuyen.Components.Core.Api.DataAccess.SqlServer
{
    /// <summary>
    /// A dynamic parameters object which supports SQL-Server-specific functionality.
    /// </summary>
    public class SqlDynamicParameters : DynamicParameters, SqlMapper.IDynamicParameters
    {
        private IDictionary<string, object[]> tableValuedParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDynamicParameters"/> class.
        /// </summary>
        public SqlDynamicParameters()
            : base()
        {
            this.tableValuedParameters = new Dictionary<string, object[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDynamicParameters"/> class.
        /// </summary>
        /// <param name="template">The template to use. Can be an anonymous type or a DynamicParameters bag</param>
        public SqlDynamicParameters(object template)
            : base(template)
        {
            this.tableValuedParameters = new Dictionary<string, object[]>();
        }

        /// <summary>
        /// Adds the data as a table-valued parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a table-valued parameter with this name has already been added.
        /// or
        /// <paramref name="name"/> is blank.
        /// </exception>
        public void AddAsTable(string name, params object[] data)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (!this.tableValuedParameters.ContainsKey(name))
                    this.tableValuedParameters.Add(name, data);
                else
                    throw new ArgumentException("A table-valued parameter with this name has already been added.", "name");
            }
            else
                throw new ArgumentException("A parameter name must be specified.", "name");
        }

        /// <summary>
        /// Adds the data as a table-valued parameter.
        /// </summary>
        /// <typeparam name="T">The type of the data</typeparam>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="System.ArgumentException">Thrown when a table-valued parameter with this name has already been added.
        /// or
        /// <paramref name="name" /> is blank.</exception>
        public void AddAsTable<T>(string name, IEnumerable<T> data)
        {
            this.AddAsTable(name, data.Cast<object>().ToArray());
        }

        /// <inheritdoc/>
        void SqlMapper.IDynamicParameters.AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            this.AddParameters(command, identity);
        }

        /// <inheritdoc/>
        protected new void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            base.AddParameters(command, identity);
            foreach (KeyValuePair<string, object[]> paramDefinition in this.tableValuedParameters)
            {
                if (paramDefinition.Value != null && paramDefinition.Value.Length > 0)
                {
                    using (DataTable table = CreateDataTable(paramDefinition.Value[0]))
                    {
                        PopulateDataTable(table, paramDefinition.Value);
                        SqlParameter sqlParameter = new SqlParameter(paramDefinition.Key, SqlDbType.Structured)
                        {
                            Direction = ParameterDirection.Input,
                            Value = table
                        };

                        ((SqlCommand)command).Parameters.Add(sqlParameter);
                    }
                }
            }
        }

        private static bool IsNullableType(Type type, out Type underlyingType)
        {
            if (type.IsValueType)
            {
                underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType != null;
            }
            else
            {
                underlyingType = null;
                return false;
            }
        }

        private static bool IsEnum(Type type, out Type underlyingType)
        {
            if (type.IsEnum)
            {
                underlyingType = Enum.GetUnderlyingType(type);
                return true;
            }
            else
            {
                underlyingType = null;
                return false;
            }
        }

        private static DataTable CreateDataTable(object firstRow)
        {
            DataTable table = new DataTable();
            Type firstRowType = firstRow.GetType();
            PropertyInfo[] properties = firstRowType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < properties.Length; i++)
            {
                string columnName = properties[i].Name;
                Type dataType = properties[i].PropertyType;
                Type underlyingType;
                if (IsNullableType(dataType, out underlyingType))
                    dataType = underlyingType;
                if (IsEnum(dataType, out underlyingType))
                    dataType = underlyingType;
                table.Columns.Add(columnName, dataType);
            }

            return table;
        }

        private static void PopulateDataTable(DataTable table, params object[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                DataRow newRow = table.NewRow();
                PropertyInfo[] properties = data[i].GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                for (int p = 0; p < properties.Length; p++)
                {
                    string columnName = properties[p].Name;
                    object value = properties[p].GetValue(data[i], null);
                    newRow[columnName] = value ?? DBNull.Value;
                }

                table.Rows.Add(newRow);
            }

            table.AcceptChanges();
        }
    }
}
