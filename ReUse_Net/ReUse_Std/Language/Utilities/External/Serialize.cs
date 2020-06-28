using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ReUse_Std.Utilities.External.Serialize
{
    /// <summary>
    /// Common Objects Serialize Utilities
    /// </summary>
    public static class SerializeUtilities
    {
        /// <summary>
        /// Serialize and Save Value with all properties to current specified CurrFilePath. Value type need to have [Serializable] attribute
        /// </summary>
        /// <param name="CurrFilePath">File Path to save Value object into.</param>
        /// <remarks><para>The Value object is saved using .NET serialization (binary formatter is used).</para></remarks>
        public static void _Fs<T>(this string CurrFilePath, T Value)
        {
            if (CurrFilePath == null)
                return;
            using (var str = new FileStream(CurrFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                _S(str, Value);
            }
        }

        /// <summary>
        /// Serialize and Save Value with all properties to current specified CurrStream. Value type need to have [Serializable] attribute
        /// </summary>
        /// <param name="CurrStream">Stream to save Value object into.</param>
        /// <remarks><para>The Value object is saved using .NET serialization (binary formatter is used).</para></remarks>
        public static void _S<T>(this Stream CurrStream, T Value)
        {
            if (CurrStream == null)
                return;
            IFormatter f = new BinaryFormatter();
            f.Serialize(CurrStream, Value);
        }

        /// <summary>
        /// Deserialize and Load Data object with all properties from current specified CurrFilePath. Data object type need to have [Serializable] attribute
        /// </summary>
        /// <param name="CurrFilePath">File Path to load Data object from.</param>
        /// <returns>Returns instance of Data object with all properties initialized from file.</returns>
        /// <remarks><para>Data object is loaded from file using .NET serialization (binary formater is used).</para></remarks>
        public static T _Fd<T>(this string CurrFilePath, T DefaultValue = default(T))
        {
            if (CurrFilePath == null)
                return DefaultValue;
            T Data = DefaultValue;
            using (var str = new FileStream(CurrFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Data = _L<T>(str);
            }
            return Data;
        }

        /// <summary>
        /// Deserialize and Load Data object with all properties from current specified CurrStream. Data object type need to have [Serializable] attribute
        /// </summary>
        /// <param name="CurrStream">Stream to load Data object from.</param>
        /// <returns>Returns instance of object with all properties initialized from file.</returns>
        /// <remarks><para>Data object is loaded from file using .NET serialization (binary formater is used).</para></remarks>
        public static T _L<T>(this Stream CurrStream, T DefaultValue = default(T))
        {
            if (CurrStream == null)
                return DefaultValue;
            IFormatter f = new BinaryFormatter();
            var v = f.Deserialize(CurrStream);
            if (v != null)
                return (T)v;
            return DefaultValue;
        }
    }
}
