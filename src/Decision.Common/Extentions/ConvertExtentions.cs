using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Decision.Common.Extentions
{
    public static class ConvertExtentions
    {
        #region Fields
        private readonly static IDictionary<Type, TypeConverter> CustomTypeConverters;
        #endregion

        #region Ctor

        static ConvertExtentions()
        {
            CustomTypeConverters = new Dictionary<Type, TypeConverter>();
        }
        #endregion

        #region Object

        public static T Convert<T>(this object value)
        {
            return (T)Convert(value, typeof(T));
        }

        public static T Convert<T>(this object value, CultureInfo culture)
        {
            return (T)Convert(value, typeof(T), culture);
        }

        public static object Convert(this object value, Type to)
        {
            return value.Convert(to, CultureInfo.InvariantCulture);
        }

        public static object Convert(this object value, Type to, CultureInfo culture)
        {
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
                    var valueAsList = (IList)valueAsArray;
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
            else if (valueAsArray != null)
            {
                // case 3: destination type is single element but source is array, so extract first element + convert
                var valueAsList = (IList)valueAsArray;
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
                if (!to.IsEnum) return System.Convert.ChangeType(value, to, culture);
                if (value is string)
                    return Enum.Parse(to, value.ToString(), true);
                return fromType.IsInteger() ? Enum.ToObject(to, value) : System.Convert.ChangeType(value, to, culture);
            }

            if (value is DateTime && to == typeof(DateTimeOffset))
                return new DateTimeOffset((DateTime)value);

            if (value is string && to == typeof(Guid))
                return new Guid((string)value);

            // see if source or target types have a TypeConverter that converts between the two
            var toConverter = GetTypeConverter(fromType);

            var nonNullableTo = to.GetNonNullableType();
            var isNullableTo = to != nonNullableTo;

            if (toConverter != null && toConverter.CanConvertTo(nonNullableTo))
            {
                var result = toConverter.ConvertTo(null, culture, value, nonNullableTo);
                return isNullableTo ? Activator.CreateInstance(typeof(Nullable<>).MakeGenericType(nonNullableTo), result) : result;
            }

            var fromConverter = GetTypeConverter(nonNullableTo);

            if (fromConverter != null && fromConverter.CanConvertFrom(fromType))
            {
                var result = fromConverter.ConvertFrom(null, culture, value);
                return isNullableTo ? Activator.CreateInstance(typeof(Nullable<>).MakeGenericType(nonNullableTo), result) : result;
            }

            // TypeConverter doesn't like Double to Decimal
            if (fromType == typeof(double) && nonNullableTo == typeof(decimal))
            {
                var result = new Decimal((double)value);
                return isNullableTo ? Activator.CreateInstance(typeof(Nullable<>).MakeGenericType(nonNullableTo), result) : result;
            }

            throw new Exception($"cann't convert from {fromType.FullName} to {to.FullName}");

        }

        internal static TypeConverter GetTypeConverter(Type type)
        {
            TypeConverter converter;
            return CustomTypeConverters.TryGetValue(type, out converter) ? converter : TypeDescriptor.GetConverter(type);
        }

        #endregion
    }
}
