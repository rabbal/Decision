using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Decision.Common.Extentions
{
    public static class TypeExtentions
    {
        /// <summary>
        /// determinate whether a targt class or method have this attribute
        /// </summary>
        /// <typeparam name="TAttribute">attributeType</typeparam>
        /// <param name="target">target class o method</param>
        /// <param name="inherits">if is true the the base classes</param>
        /// <returns></returns>
        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider target, bool inherits) where TAttribute : Attribute
        {
            return target.IsDefined(typeof(TAttribute), inherits);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
        public static bool IsInteger(this Type type)
        {

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the underlying type of a <see cref="Nullable{T}" /> type.
        /// </summary>
        public static Type GetNonNullableType(this Type type)
        {
            return !IsNullable(type) ? type : type.GetGenericArguments()[0];
        }

        public static bool IsNullable(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        public static TypeConverter GetTypeConverter(Type type)
        {
            return ConvertExtentions.GetTypeConverter(type);
        }

        /// <summary>
        /// get specific attribute for type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T GetCustomAttribut<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttributes(true)
                .FirstOrDefault(x => x is T) as
                T;
        }
    }
}
