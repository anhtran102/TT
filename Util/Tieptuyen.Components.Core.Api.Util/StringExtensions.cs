using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// Provides extension methods for the <see cref="String"/> objects.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly IDictionary<char, string> XmlEscapes;

        /// <summary>
        /// Initializes static members of the <see cref="StringExtensions" /> class.
        /// </summary>
        static StringExtensions()
        {
            XmlEscapes = CreateXmlEscapesLookup();
        }        

        /// <summary>
        /// Nullifies if the string if it is empty.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns><c>null</c> if <paramref name="value"/> is empty; otherwise it returns <paramref name="value"/>.</returns>
        public static string NullifyIfEmpty(this string value)
        {
            if (value != null)
                return value.Length > 0 ? value : (string)null;
            else
                return value;
        }

        /// <summary>
        /// Initializes the string if it is <c>null</c>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>An empty string, if <paramref name="value"/> is <c>null</c>; otherwise it returns <paramref name="value"/>.</returns>
        public static string InitializeIfNull(this string value)
        {
            return value != null ? value : string.Empty;
        }

        /// <summary>
        /// Escapes the specified string for XML.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The escaped <see cref="String"/>.</returns>
        public static string EscapeForXml(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                StringBuilder escapedStringBuilder = new StringBuilder();
                for (int i = 0; i < value.Length; i++)
                {
                    if (XmlEscapes.ContainsKey(value[i]))
                        escapedStringBuilder.Append(XmlEscapes[value[i]]);
                    else
                        escapedStringBuilder.Append(value[i]);
                }

                return escapedStringBuilder.ToString();
            }
            else
                return value;
        }

        /// <summary>
        /// Performs a case-insensitive string replacement.
        /// </summary>
        /// <param name="value">The string value to perform the replacement on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The modified string.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="oldValue"/> is either <c>null</c> or empty.</exception>
        public static string CaseInsensitiveReplace(this string value, string oldValue, string newValue)
        {            
            if (!string.IsNullOrEmpty(value))
            {
                if (string.IsNullOrEmpty(oldValue))
                    throw new ArgumentException("The search string cannot be null or empty.", "oldValue");
                if (newValue == null)
                    newValue = string.Empty;
                int index = value.IndexOf(oldValue, StringComparison.InvariantCultureIgnoreCase);
                if (index > -1)
                {
                    string preReplace = value.Substring(0, index);
                    string postReplace = value.Length > index + oldValue.Length  ? value.Substring(index + oldValue.Length) : string.Empty;
                    return string.Concat(preReplace, newValue, postReplace);
                }
                else
                    return value;
            }
            else
                return value;
        }

        private static IDictionary<char, string> CreateXmlEscapesLookup()
        {
            return new Dictionary<char, string>()
            {
                { '"', "&quot;" },
                { '&', "&amp;" },
                { '\'', "&apos;" },
                { '<', "&lt;" },
                { '>', "&gt;" }
            };
        }
    }
}
