using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using NTierMvcFramework.Common.Infrastructure;

namespace NTierMvcFramework.Common.Extensions
{
    public static class ConversionExtensions
    {
        private static readonly IDictionary<Type, TypeConverter> _customTypeConverters;

        static ConversionExtensions()
        {
            _customTypeConverters = new Dictionary<Type, TypeConverter>();
        }

        #region Object

        public static T Convert<T>(this object value)
        {
            return (T) Convert(value, typeof(T));
        }

        public static T Convert<T>(this object value, CultureInfo culture)
        {
            return (T) Convert(value, typeof(T), culture);
        }

        public static object Convert(this object value, Type to)
        {
            return value.Convert(to, CultureInfo.InvariantCulture);
        }

        public static object Convert(this object value, Type to, CultureInfo culture)
        {
            Guard.ArgumentNotNull(to, nameof(to));

            if (value == null || to.IsInstanceOfType(value))
            {
                return value;
            }

            // array conversion results in four cases, as below
            var valueAsArray = value as Array;
            if (to.IsArray)
            {
                var destinationElementType = to.GetElementType();
                if (valueAsArray != null)
                {
                    // case 1: both destination + source type are arrays, so convert each element
                    var valueAsList = (IList) valueAsArray;
                    IList converted = Array.CreateInstance(destinationElementType, valueAsList.Count);
                    for (var i = 0; i < valueAsList.Count; i++)
                    {
                        converted[i] = valueAsList[i].Convert(destinationElementType, culture);
                    }
                    return converted;
                }
                else
                {
                    // case 2: destination type is array but source is single element, so wrap element in array + convert
                    var element = value.Convert(destinationElementType, culture);
                    IList converted = Array.CreateInstance(destinationElementType, 1);
                    converted[0] = element;
                    return converted;
                }
            }
            if (valueAsArray != null)
            {
                // case 3: destination type is single element but source is array, so extract first element + convert
                var valueAsList = (IList) valueAsArray;
                if (valueAsList.Count > 0)
                {
                    value = valueAsList[0];
                }
                // .. fallthrough to case 4
            }
            // case 4: both destination + source type are single elements, so convert

            var fromType = value.GetType();

            //if (to.IsInterface || to.IsGenericTypeDefinition || to.IsAbstract)
            //	throw Error.Argument("to", "Target type '{0}' is not a value type or a non-abstract class.", to.FullName);

            // use Convert.ChangeType if both types are IConvertible
            if (value is IConvertible && typeof(IConvertible).IsAssignableFrom(to))
            {
                if (to.IsEnum)
                {
                    if (value is string)
                        return Enum.Parse(to, value.ToString(), true);
                    if (fromType.IsInteger())
                        return Enum.ToObject(to, value);
                }

                return System.Convert.ChangeType(value, to, culture);
            }

            if (value is DateTime && to == typeof(DateTimeOffset))
                return new DateTimeOffset((DateTime) value);

            if (value is string && to == typeof(Guid))
                return new Guid((string) value);

            // see if source or target types have a TypeConverter that converts between the two
            var toConverter = GetTypeConverter(fromType);

            var nonNullableTo = to.GetNonNullableType();
            var isNullableTo = to != nonNullableTo;

            if (toConverter != null && toConverter.CanConvertTo(nonNullableTo))
            {
                var result = toConverter.ConvertTo(null, culture, value, nonNullableTo);
                return isNullableTo
                    ? Activator.CreateInstance(typeof(Nullable<>).MakeGenericType(nonNullableTo), result)
                    : result;
            }

            var fromConverter = GetTypeConverter(nonNullableTo);

            if (fromConverter != null && fromConverter.CanConvertFrom(fromType))
            {
                var result = fromConverter.ConvertFrom(null, culture, value);
                return isNullableTo
                    ? Activator.CreateInstance(typeof(Nullable<>).MakeGenericType(nonNullableTo), result)
                    : result;
            }

            // TypeConverter doesn't like Double to Decimal
            if (fromType == typeof(double) && nonNullableTo == typeof(decimal))
            {
                var result = new decimal((double) value);
                return isNullableTo
                    ? Activator.CreateInstance(typeof(Nullable<>).MakeGenericType(nonNullableTo), result)
                    : result;
            }

            throw Error.InvalidCast(fromType, to);

            #region OBSOLETE

            //            TypeConverter converter = TypeDescriptor.GetConverter(to);
            //            bool canConvertFrom = converter.CanConvertFrom(value.GetType());
            //            if (!canConvertFrom)
            //            {
            //                converter = TypeDescriptor.GetConverter(value.GetType());
            //            }
            //            if (!(canConvertFrom || converter.CanConvertTo(to)))
            //            {
            //                throw Error.InvalidOperation(@"The parameter conversion from type '{0}' to type '{1}' failed 
            //                                         because no TypeConverter can convert between these types.",
            //                                         value.GetType().FullName,
            //                                         to.FullName);
            //            }

            //            try
            //            {
            //                CultureInfo cultureToUse = culture ?? CultureInfo.CurrentCulture;
            //                object convertedValue = (canConvertFrom) ?
            //                     converter.ConvertFrom(null /* context */, cultureToUse, value) :
            //                     converter.ConvertTo(null /* context */, cultureToUse, value, to);
            //                return convertedValue;
            //            }
            //            catch (Exception ex)
            //            {
            //                throw Error.InvalidOperation(@"The parameter conversion from type '{0}' to type '{1}' failed. 
            //                                         See the inner exception for more information.", ex,
            //                                         value.GetType().FullName,
            //                                         to.FullName);
            //            }

            #endregion
        }

        internal static TypeConverter GetTypeConverter(Type type)
        {
            TypeConverter converter;
            return _customTypeConverters.TryGetValue(type, out converter)
                ? converter
                : TypeDescriptor.GetConverter(type);
        }

        #endregion

        #region int

        public static char ToHex(this int value)
        {
            if (value <= 9)
            {
                return (char) (value + 48);
            }
            return (char) (value - 10 + 97);
        }

        /// <summary>
        ///     Returns kilobytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToKb(this int value)
        {
            return value*1024;
        }

        /// <summary>
        ///     Returns megabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToMb(this int value)
        {
            return value*1024*1024;
        }

        /// <summary>Returns a <see cref="TimeSpan" /> that represents a specified number of minutes.</summary>
        /// <param name="minutes">number of minutes</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        /// <example>3.Minutes()</example>
        public static TimeSpan ToMinutes(this int minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        /// <summary>
        ///     Returns a <see cref="TimeSpan" /> that represents a specified number of seconds.
        /// </summary>
        /// <param name="seconds">number of seconds</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        /// <example>2.Seconds()</example>
        public static TimeSpan ToSeconds(this int seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        /// <summary>
        ///     Returns a <see cref="TimeSpan" /> that represents a specified number of milliseconds.
        /// </summary>
        /// <param name="milliseconds">milliseconds for this timespan</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        public static TimeSpan ToMilliseconds(this int milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        /// <summary>
        ///     Returns a <see cref="TimeSpan" /> that represents a specified number of days.
        /// </summary>
        /// <param name="days">Number of days.</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        public static TimeSpan ToDays(this int days)
        {
            return TimeSpan.FromDays(days);
        }


        /// <summary>
        ///     Returns a <see cref="TimeSpan" /> that represents a specified number of hours.
        /// </summary>
        /// <param name="hours">Number of hours.</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        public static TimeSpan ToHours(this int hours)
        {
            return TimeSpan.FromHours(hours);
        }

        #endregion

        #region double

        /// <summary>Returns a <see cref="TimeSpan" /> that represents a specified number of minutes.</summary>
        /// <param name="minutes">number of minutes</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        /// <example>3D.Minutes()</example>
        public static TimeSpan ToMinutes(this double minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }


        /// <summary>Returns a <see cref="TimeSpan" /> that represents a specified number of hours.</summary>
        /// <param name="hours">number of hours</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        /// <example>3D.Hours()</example>
        public static TimeSpan ToHours(this double hours)
        {
            return TimeSpan.FromHours(hours);
        }

        /// <summary>Returns a <see cref="TimeSpan" /> that represents a specified number of seconds.</summary>
        /// <param name="seconds">number of seconds</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        /// <example>2D.Seconds()</example>
        public static TimeSpan ToSeconds(this double seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        /// <summary>Returns a <see cref="TimeSpan" /> that represents a specified number of milliseconds.</summary>
        /// <param name="milliseconds">milliseconds for this timespan</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        public static TimeSpan ToMilliseconds(this double milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        /// <summary>
        ///     Returns a <see cref="TimeSpan" /> that represents a specified number of days.
        /// </summary>
        /// <param name="days">Number of days, accurate to the milliseconds.</param>
        /// <returns>A <see cref="TimeSpan" /> that represents a value.</returns>
        public static TimeSpan ToDays(this double days)
        {
            return TimeSpan.FromDays(days);
        }

        #endregion

        #region String

        public static T ToEnum<T>(this string value, T defaultValue) where T : IComparable, IFormattable
        {
            var convertedValue = defaultValue;

            if (string.IsNullOrEmpty(value)) return convertedValue;
            {
                convertedValue = (T) Enum.Parse(typeof(T), value.Trim(), true);
            }

            return convertedValue;
        }

        public static string[] ToArray(this string value)
        {
            return value.ToArray(',');
        }

        public static string[] ToArray(this string value, params char[] separator)
        {
            return value.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int ToInt(this string value, int defaultValue = 0)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return defaultValue;
        }

        public static float ToFloat(this string value, float defaultValue = 0)
        {
            float result;
            return float.TryParse(value, out result) ? result : defaultValue;
        }

        public static bool ToBool(this string value, bool defaultValue = false)
        {
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            return defaultValue;
        }

        public static DateTime? ToDateTime(this string value, DateTime? defaultValue)
        {
            return value.ToDateTime(null, defaultValue);
        }

        public static DateTime? ToDateTime(this string value, string[] formats, DateTime? defaultValue)
        {
            return value.ToDateTime(formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces,
                defaultValue);
        }

        public static DateTime? ToDateTime(this string value, string[] formats, IFormatProvider provider,
            DateTimeStyles styles, DateTime? defaultValue)
        {
            DateTime result;

            if (formats.IsNullOrEmpty())
            {
                if (DateTime.TryParse(value, provider, styles, out result))
                {
                    return result;
                }
            }

            if (DateTime.TryParseExact(value, formats, provider, styles, out result))
            {
                return result;
            }

            return defaultValue;
        }


        public static Guid ToGuid(this string value)
        {
            if (string.IsNullOrEmpty(value) || (value.Trim().Length != 22)) return Guid.Empty;
            var encoded = string.Concat(value.Trim().Replace("-", "+").Replace("_", "/"), "==");

            var base64 = System.Convert.FromBase64String(encoded);

            return new Guid(base64);
        }

        public static byte[] ToByteArray(this string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        [DebuggerStepThrough]
        public static Version ToVersion(this string value, Version defaultVersion = null)
        {
            try
            {
                return new Version(value);
            }
            catch (Exception)
            {
                return defaultVersion ?? new Version("1.0");
            }
        }

        #endregion

        #region DateTime

        // [...]

        #endregion

        #region Stream

        public static byte[] ToByteArray(this Stream stream)
        {
            Guard.ArgumentNotNull(stream, nameof(stream));

            byte[] buffer;

            if (stream is MemoryStream && stream.CanRead && stream.CanSeek)
            {
                var len = System.Convert.ToInt32(stream.Length);
                buffer = new byte[len];
                stream.Read(buffer, 0, len);
                return buffer;
            }
            using (var memStream = new MemoryStream())
            {
                buffer = new byte[1024];

                var bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead <= 0) return memStream.ToArray();

                memStream.Write(buffer, 0, bytesRead);
                stream.Read(buffer, 0, buffer.Length);

                return memStream.ToArray();
            }
        }

        public static string AsString(this Stream stream)
        {
            // convert memory stream to string
            string result;
            stream.Position = 0;

            using (var sr = new StreamReader(stream))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        #endregion

        #region ByteArray

        /// <summary>
        ///     Converts a byte array into an object.
        /// </summary>
        /// <param name="bytes">Object to deserialize. May be null.</param>
        /// <returns>Deserialized object, or null if input was null.</returns>
        public static object ToObject(this byte[] bytes)
        {
            if (bytes == null)
                return null;

            using (var stream = new MemoryStream(bytes))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }

        public static Image ToImage(this byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return Image.FromStream(stream);
            }
        }

        public static Stream ToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static string AsString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }


        [DebuggerStepThrough]
        public static string Hash(this byte[] value, bool toBase64 = false)
        {
            Guard.ArgumentNotNull(value, nameof(value));

            using (var md5 = MD5.Create())
            {
                if (toBase64)
                {
                    var hash = md5.ComputeHash(value);
                    return System.Convert.ToBase64String(hash);
                }
                var sb = new StringBuilder();

                var hashBytes = md5.ComputeHash(value);
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2").ToLower());
                }

                return sb.ToString();
            }
        }

        #endregion

        #region Image/Bitmap

        public static byte[] ToByteArray(this Image image)
        {
            Guard.ArgumentNotNull(() => image);

            var converter = new ImageConverter();
            var bytes = (byte[]) converter.ConvertTo(image, typeof(byte[]));
            return bytes;
        }


        internal static Image ConvertTo(this Image image, ImageFormat format)
        {
            Guard.ArgumentNotNull(() => image);
            Guard.ArgumentNotNull(() => format);

            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return Image.FromStream(stream);
            }
        }

        #endregion
    }
}