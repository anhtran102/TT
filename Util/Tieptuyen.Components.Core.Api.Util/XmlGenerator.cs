using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// A simple XML generator.
    /// </summary>
    public class XmlGenerator
    {
        private static readonly ISet<Type> BuiltInTypes;

        /// <summary>
        /// Initializes static members of the <see cref="XmlGenerator"/> class.
        /// </summary>
        static XmlGenerator()
        {
            Current = new XmlGenerator();
            BuiltInTypes = CreateBuiltInTypesLookup();
        }        

        /// <summary>
        /// Prevents a default instance of the <see cref="XmlGenerator"/> class from being created.
        /// </summary>
        private XmlGenerator()
        {
        }

        /// <summary>
        /// Gets the current XML generator.
        /// </summary>
        public static XmlGenerator Current { get; private set; }

        /// <summary>
        /// Helper method for generating XML.
        /// </summary>
        /// <param name="data">The data to be represented by the generated XML.</param>
        /// <returns>The generated XML as a <see cref="String"/>; or <c>null</c> if <paramref name="data"/> is <c>null</c>.</returns>
        /// <exception cref="System.Xml.XmlException">Throw if the data does not have one, and only one, root element.</exception>
        public string GenerateXml(object data)
        {
            if (data != null)
            {
                PropertyInfo[] root = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (root.Length == 1)
                {
                    StringBuilder xmlBuilder = new StringBuilder();
                    GenerateXml(root[0], data, xmlBuilder);
                    return xmlBuilder.ToString();
                }
                else
                    throw new XmlException("The data must have one, and only one, root element.");
            }
            else
                return string.Empty;
        }

        private static void GenerateXml(PropertyInfo property, object data, StringBuilder xmlBuilder)
        {
            string elementName = property.Name;
            object value = property.GetValue(data, null);
            if (value != null)
            {
                xmlBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<{0}", elementName));
                string stringValue = value as string;
                IEnumerable enumerableValue = value as IEnumerable;
                Type underLyingType;
                if (stringValue != null)
                {
                    xmlBuilder.Append('>');
                    xmlBuilder.Append(stringValue.EscapeForXml());
                }                
                else if (IsBuiltInType(value))
                {
                    xmlBuilder.Append('>');
                    xmlBuilder.Append(value.ToCultureInvariantString());
                }
                else if (IsEnum(value, out underLyingType))
                {
                    xmlBuilder.Append('>');
                    xmlBuilder.Append(Convert.ChangeType(value, underLyingType).ToCultureInvariantString());
                }
                else if (enumerableValue != null)
                {
                    xmlBuilder.Append('>');
                    foreach (object childObject in enumerableValue)
                    {
                        PropertyInfo[] properties = childObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        for (int i = 0; i < properties.Length; i++)
                            GenerateXml(properties[i], childObject, xmlBuilder);
                    }
                }
                else
                {
                    PropertyInfo[] childObjects = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo attribute in childObjects.Where(x => x.Name.StartsWith("_")))
                        xmlBuilder.Append(string.Format(CultureInfo.InvariantCulture, " {0}=\"{1}\"", attribute.Name.Substring(1), GenerateAttributeValue(attribute.GetValue(value, null))));
                    xmlBuilder.Append('>');
                    foreach (PropertyInfo childElement in childObjects.Where(x => !x.Name.StartsWith("_")))
                        GenerateXml(childElement, value, xmlBuilder);
                }

                xmlBuilder.Append(string.Format(CultureInfo.InvariantCulture, "</{0}>", elementName));
            }
        }

        private static string GenerateAttributeValue(object value)
        {
            string stringValue = value as string;
            Type underLyingType;
            if (stringValue != null)
                return stringValue.EscapeForXml();
            else if (IsEnum(value, out underLyingType))
                return Convert.ChangeType(value, underLyingType).ToCultureInvariantString();
            else
                return value.ToCultureInvariantString();
        }

        private static bool IsBuiltInType(object value)
        {
            return BuiltInTypes.Contains(value.GetType());
        }

        private static bool IsEnum(object value, out Type underlyingType)
        {
            Type type = value.GetType();
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

        private static ISet<Type> CreateBuiltInTypesLookup()
        {
            return new HashSet<Type>()
            {
                typeof(bool),
                typeof(bool?),
                typeof(byte),
                typeof(byte?),
                typeof(short),
                typeof(short?),
                typeof(int),
                typeof(int?),
                typeof(long),
                typeof(long?),
                typeof(float),
                typeof(float?),
                typeof(double),
                typeof(double?),
                typeof(decimal),
                typeof(decimal?),
                typeof(DateTime),
                typeof(DateTime?),
                typeof(DateTimeOffset),
                typeof(DateTimeOffset?),
                typeof(TimeSpan),
                typeof(TimeSpan?),
                typeof(Guid),
                typeof(Guid?)
            };
        }
    }
}
