using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tieptuyen.Components.Core.Api.Util
{
    /// <summary>
    /// Provides extension methods for all objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns the culture-invariant string representation of an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The culture-invariant <see cref="String"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="obj"/> is <c>null</c>.</exception>
        public static string ToCultureInvariantString(this object obj)
        {
            if (obj != null)
            {
                Type objType = obj.GetType();
                if (objType == typeof(DateTime))
                {
                    return ((DateTime)obj).ToString("O", CultureInfo.InvariantCulture);
                }
                else
                {
                    MethodInfo toStringMethod = objType.GetMethod("ToString", new Type[] { typeof(IFormatProvider) });
                    if (toStringMethod != null)
                        return (string)toStringMethod.Invoke(obj, new object[] { CultureInfo.InvariantCulture });
                    else
                        return obj.ToString();
                }
            }
            else
                throw new ArgumentNullException("obj", "The object cannot be null.");
        }

        /// <summary>
        /// Deep clones an object that is marked as serializable.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj">The object</param>
        /// <returns>A cloned object</returns>
        public static T DeepClone<T>(this T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}