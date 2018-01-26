using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Dapper;
using Tieptuyen.Components.Core.Api.Util;

using SystemConfigManager = System.Configuration.ConfigurationManager;

namespace Tieptuyen.Components.Core.Api.Configuration.Default
{
    /// <summary>
    /// A manager for handling configuration values in the system.
    /// </summary>
    public class ConfigurationManager : DynamicObject, IConfigurationManager
    {
        private string connectionString;
        private IDictionary<string, object> cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        public ConfigurationManager()
        {
            this.connectionString = SystemConfigManager.ConnectionStrings["kDataConnection"].ConnectionString;
            this.cache = new Dictionary<string, object>();
        }

        /// <inheritdoc/>
        public IDictionary<string, object> GetAllValues()
        {
            return this.ReadFromDatabase(null);
        }

        /// <inheritdoc/>
        public object GetValue(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                IDictionary<string, object> result = this.ReadFromDatabase(new string[] { key });
                if (result.Count == 1)
                    return result[key];
                else
                    return null;
            }
            else
                throw new ArgumentException("The key cannot be null or empty.", "key");
        }

        /// <inheritdoc/>
        public IDictionary<string, object> GetValues(string[] keys)
        {
            if (keys != null && keys.Length > 0)
                return this.ReadFromDatabase(keys);
            else
                throw new ArgumentException("The array of keys cannot be null or empty.", "keys");
        }

        /// <inheritdoc/>
        public void SetValue(string key, object value, ValueType type)
        {
            this.WriteToDatabase(key, value, type);
        }

        /// <inheritdoc/>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (this.cache.ContainsKey(binder.Name))
                result = this.cache[binder.Name];
            else
            {
                result = this.GetValue(binder.Name);
                this.cache.Add(binder.Name, result);
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            ValueType type = GetType(value);
            this.SetValue(binder.Name, value, type);
            this.cache[binder.Name] = value;
            return true;
        }

        private static object GetValue(ConfigurationValue value)
        {
            if (value.BoolValue.HasValue)
                return value.BoolValue.Value;
            else if (value.IntValue.HasValue)
                return value.IntValue.Value;
            else if (value.FloatValue.HasValue)
                return value.FloatValue.Value;
            else if (value.DateValue.HasValue)
                return value.DateValue.Value;
            else if (value.DatetimeValue.HasValue)
                return value.DatetimeValue.Value;
            else if (value.StringValue != null)
                return value.StringValue;
            else if (value.XMLValue != null)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(value.XMLValue);
                return xml;
            }
            else
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Value '{0}' is of an unsuppored type.", value.ConfigurationID));
        }

        private static ValueType GetType(object value)
        {
            if (value is bool)
                return ValueType.Boolean;
            else if (value is int)
                return ValueType.Integer;
            else if (value is double)
                return ValueType.Float;
            else if (value is DateTime)
            {
                DateTime dateValue = (DateTime)value;
                if (dateValue.HasTimePortion())
                    return ValueType.DateTime;
                else
                    return ValueType.Date;
            }
            else if (value is string)
                return ValueType.String;
            else if (value is XmlDocument)
                return ValueType.Xml;
            else
                throw new InvalidOperationException("Configuration values of this type are not supported.");
        }

        private static void SetValueParameter(DynamicParameters parameters, ValueType type, object value)
        {
            switch (type)
            {
                case ValueType.Boolean:
                    parameters.Add("BoolValue", value);
                    break;
                case ValueType.Integer:
                    parameters.Add("IntValue", value);
                    break;
                case ValueType.Float:
                    parameters.Add("FloatValue", value);
                    break;
                case ValueType.Date:
                    parameters.Add("DateValue", value);
                    break;
                case ValueType.DateTime:
                    parameters.Add("DatetimeValue", value);
                    break;
                case ValueType.String:
                    parameters.Add("StringValue", value);
                    break;
                case ValueType.Xml:
                    parameters.Add("XMLValue", value);
                    break;
            }
        }

        private IDictionary<string, object> ReadFromDatabase(IEnumerable<string> keys)
        {
            const string StoredProcedure = "cmn.GetConfigurationValues";
            IDictionary<string, object> configurationValues = new Dictionary<string, object>();
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                if (keys != null)
                {
                    foreach (string key in keys)
                    {
                        var parameters = new { ConfigurationID = key };
                        ConfigurationValue result = connection.Query<ConfigurationValue>(StoredProcedure, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                        if (result != null)
                            configurationValues.Add(result.ConfigurationID, GetValue(result));
                    }
                }
                else
                {
                    IEnumerable<ConfigurationValue> result = connection.Query<ConfigurationValue>(StoredProcedure, null, commandType: CommandType.StoredProcedure);
                    result.Each(
                        x =>
                        {
                            object value = GetValue(x);
                            configurationValues.Add(x.ConfigurationID, value);
                        });
                }
            }

            return configurationValues;
        }

        private void WriteToDatabase(string key, object value, ValueType type)
        {
            if (!string.IsNullOrEmpty(key))
            {
                const string StoredProcedure = "cmn.SetConfigurationValue";
                DynamicParameters parameters = new DynamicParameters(new { ConfigurationID = key });
                SetValueParameter(parameters, type, value);
                using (IDbConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            else
                throw new ArgumentNullException("key", "The key cannot be null.");
        }

        private sealed class ConfigurationValue
        {
            public string ConfigurationID { get; set; }

            public bool? BoolValue { get; set; }

            public int? IntValue { get; set; }

            public double? FloatValue { get; set; }

            public DateTime? DateValue { get; set; }

            public DateTime? DatetimeValue { get; set; }

            public string StringValue { get; set; }

            public string XMLValue { get; set; }
        }        
    }
}
