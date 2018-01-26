using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// The descriptor for a parsing operation. 
    /// </summary>
    /// <typeparam name="T">The type to parse the input as.</typeparam>
    public class ParseDescriptor<T>
        where T : struct
    {
        private static IDictionary<Type, Delegate> parseMethods;
        private static IDictionary<Type, Delegate> tryParseMethods;

        private Type targetType;
        private string value;

        /// <summary>
        /// Initializes static members of the <see cref="ParseDescriptor{T}"/> class.
        /// </summary>
        static ParseDescriptor()
        {
            parseMethods = new Dictionary<Type, Delegate>();
            tryParseMethods = new Dictionary<Type, Delegate>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseDescriptor{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal ParseDescriptor(string value)
        {            
            this.value = value;
            this.targetType = typeof(T);
        }

        private delegate T ParseMethod(string value);

        private delegate bool TryParseMethod(string value, out T returnValue);

        /// <summary>
        /// Performs the parse with a default value.
        /// </summary>
        /// <returns>The parsed value or the default value for <typeparamref name="T"/> if the string cannot be parsed.</returns>
        public T WithDefault()
        {
            return this.WithDefault(default(T));
        }

        /// <summary>
        /// Performs the parse with a default value.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The parsed value or <paramref name="defaultValue"/> if the string cannot be parsed.</returns>
        public T WithDefault(T defaultValue)
        {
            if (!string.IsNullOrEmpty(this.value))
            {
                if (this.targetType.IsEnum)
                {
                    T returnVal;
                    if (Enum.TryParse<T>(this.value, out returnVal))
                        return returnVal;
                    else
                        return defaultValue;
                }
                else
                    return this.TryParseNonEnum(ref defaultValue);
            }
            else
                return defaultValue;
        }        

        /// <summary>
        /// Performs the parse with no default value.
        /// </summary>
        /// <returns>The parsed value.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if <typeparamref name="T"/> doesn't support parsing.</exception>
        public T WithNoDefault()
        {
            if (this.targetType.IsEnum)
                return (T)Enum.Parse(this.targetType, this.value);
            else
                return this.ParseNonEnum();
        }

        private T TryParseNonEnum(ref T defaultValue)
        {
            TryParseMethod tryParse;
            if (tryParseMethods.ContainsKey(this.targetType))
                tryParse = (TryParseMethod)tryParseMethods[this.targetType];
            else
            {
                MethodInfo parseMethod = this.targetType.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string), this.targetType.MakeByRefType() }, null);
                if (parseMethod != null)
                {
                    tryParse = (TryParseMethod)Delegate.CreateDelegate(typeof(TryParseMethod), null, parseMethod);
                    tryParseMethods.Add(this.targetType, tryParse);
                }
                else
                    return defaultValue;
            }

            T returnValue;
            if (tryParse(this.value, out returnValue))
                return returnValue;
            else
                return defaultValue;
        }

        private T ParseNonEnum()
        {
            ParseMethod parse;
            if (parseMethods.ContainsKey(this.targetType))
                parse = (ParseMethod)parseMethods[this.targetType];
            else
            {
                MethodInfo parseMethod = this.targetType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
                if (parseMethod != null)
                {
                    parse = (ParseMethod)Delegate.CreateDelegate(typeof(ParseMethod), null, parseMethod);
                    parseMethods.Add(this.targetType, parse);
                }
                else
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The specified type, {0}, does not support parsing.", this.targetType.Name));
            }

            return parse(this.value);
        }
    }
}
