using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// Provides extension methods for performing XML serialization and de-serialization.
    /// </summary>
    public static class XmlSerialization
    {
        /// <summary>
        /// Serializes an object as XML.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized object as an XML <see cref="String"/>; or <c>null</c> if <paramref name="obj"/> is <c>null</c>.</returns>
        public static string SerializeAsXml<T>(this T obj)
        {
            if (obj != null)
                return Serialize(obj, typeof(T));
            else
                return string.Empty;
        }

        /// <summary>
        /// Deserializes the string as the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize as.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="xml"/> is <c>null</c> or empty.</exception>
        public static T DeserializeAs<T>(this string xml)
        {
            if (!string.IsNullOrEmpty(xml))
                return (T)Deserialize(xml, typeof(T));
            else
                throw new ArgumentException("The XML string cannot be null or empty.", "xml");
        }

        private static string Serialize(object obj, Type type)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    serializer.Serialize(xmlWriter, obj);
                    xmlWriter.Close();
                }

                stringWriter.Close();
            }

            return stringBuilder.ToString();
        }

        private static object Deserialize(string xml, Type type)
        {
            using (XmlTextReader xmlReader = new XmlTextReader(new StringReader(xml)))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(xmlReader);
            }
        }
    }
}
