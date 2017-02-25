using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Decision.Framework.GuardToolkit;

namespace Decision.Framework.Extensions
{
    public static class TypeExtensions
    {
        private static readonly Type[] _predefinedTypes;
        private static readonly Type[] _predefinedGenericTypes;

        static TypeExtensions()
        {
            _predefinedTypes = new[] {typeof(string), typeof(decimal), typeof(DateTime), typeof(TimeSpan), typeof(Guid)};
            _predefinedGenericTypes = new[] {typeof(Nullable<>)};
        }

        public static string AssemblyQualifiedNameWithoutVersion(this Type type)
        {
            var strArray = type.AssemblyQualifiedName.Split(',');
            return $"{strArray[0]},{strArray[1]}";
        }

        public static bool IsSequenceType(this Type seqType)
        {
            return (seqType != typeof(string))
                   && (seqType != typeof(byte[]))
                   && (seqType != typeof(char[]))
                   && (FindIEnumerable(seqType) != null);
        }

        public static bool IsPredefinedSimpleType(this Type type)
        {
            if (type.IsPrimitive && (type != typeof(IntPtr)) && (type != typeof(UIntPtr)))
            {
                return true;
            }
            if (type.IsEnum)
            {
                return true;
            }
            return _predefinedTypes.Any(t => t == type);
            //foreach (Type type2 in s_predefinedTypes)
            //{
            //    if (type2 == type)
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

        public static bool IsStruct(this Type type)
        {
            if (type.IsValueType)
            {
                return !type.IsPredefinedSimpleType();
            }
            return false;
        }

        public static bool IsPredefinedGenericType(this Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericTypeDefinition();
            }
            else
            {
                return false;
            }
            return _predefinedGenericTypes.Any(t => t == type);
            //foreach (Type type2 in s_predefinedGenericTypes)
            //{
            //    if (type2 == type)
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

        public static bool IsPredefinedType(this Type type)
        {
            if (!IsPredefinedSimpleType(type) && !IsPredefinedGenericType(type) && type != typeof(byte[]))
            {
                return string.Compare(type.FullName, "System.Xml.Linq.XElement", StringComparison.Ordinal) == 0;
            }
            return true;
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

        public static bool IsNullable(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNullAssignable(this Type type)
        {
            return !type.IsValueType || type.IsNullable();
        }

        public static bool IsConstructable(this Type type)
        {
            Check.ArgumentNotNull(type, nameof(type));

            if (type.IsAbstract || type.IsInterface || type.IsArray || type.IsGenericTypeDefinition ||
                type == typeof(void))
                return false;

            if (!HasDefaultConstructor(type))
                return false;

            return true;
        }

        [DebuggerStepThrough]
        public static bool IsAnonymous(this Type type)
        {
            if (!type.IsGenericType) return false;

            var d = type.GetGenericTypeDefinition();

            if (!d.IsClass || !d.IsSealed || !d.Attributes.HasFlag(TypeAttributes.NotPublic)) return false;

            var attributes = d.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);

            return attributes.Length > 0;
        }

        [DebuggerStepThrough]
        public static bool HasDefaultConstructor(this Type type)
        {
            Check.ArgumentNotNull(() => type);

            if (type.IsValueType)
                return true;

            return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .Any(ctor => ctor.GetParameters().Length == 0);
        }

        public static bool IsSubClass(this Type type, Type check)
        {
            Type implementingType;
            return IsSubClass(type, check, out implementingType);
        }

        public static bool IsSubClass(this Type type, Type check, out Type implementingType)
        {
            Check.ArgumentNotNull(type, nameof(type));
            Check.ArgumentNotNull(check, nameof(check));

            return IsSubClassInternal(type, type, check, out implementingType);
        }

        private static bool IsSubClassInternal(Type initialType, Type currentType, Type check, out Type implementingType)
        {
            if (currentType == check)
            {
                implementingType = currentType;
                return true;
            }

            // don't get interfaces for an interface unless the initial type is an interface
            if (check.IsInterface && (initialType.IsInterface || currentType == initialType))
            {
                foreach (var t in currentType.GetInterfaces())
                {
                    if (IsSubClassInternal(initialType, t, check, out implementingType))
                    {
                        // don't return the interface itself, return it's implementor
                        if (check == implementingType)
                            implementingType = currentType;

                        return true;
                    }
                }
            }

            if (currentType.IsGenericType && !currentType.IsGenericTypeDefinition)
            {
                if (IsSubClassInternal(initialType, currentType.GetGenericTypeDefinition(), check, out implementingType))
                {
                    implementingType = currentType;
                    return true;
                }
            }

            if (currentType.BaseType == null)
            {
                implementingType = null;
                return false;
            }

            return IsSubClassInternal(initialType, currentType.BaseType, check, out implementingType);
        }

        public static bool IsIndexed(this PropertyInfo property)
        {
            Check.ArgumentNotNull(property, nameof(property));
            return !property.GetIndexParameters().IsNullOrEmpty();
        }

        /// <summary>
        ///     Determines whether the member is an indexed property.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>
        ///     <c>true</c> if the member is an indexed property; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIndexed(this MemberInfo member)
        {
            Check.ArgumentNotNull(member, nameof(member));

            var propertyInfo = member as PropertyInfo;

            if (propertyInfo != null)
                return propertyInfo.IsIndexed();
            return false;
        }

        /// <summary>
        ///     Checks to see if the specified type is assignable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsType<TType>(this Type type)
        {
            return typeof(TType).IsAssignableFrom(type);
        }


        public static MemberInfo GetSingleMember(this Type type, string name, MemberTypes memberTypes)
        {
            return type.GetSingleMember(
                name,
                memberTypes,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        }

        public static MemberInfo GetSingleMember(this Type type, string name, MemberTypes memberTypes,
            BindingFlags bindingAttr)
        {
            return type.GetMember(
                name,
                memberTypes,
                bindingAttr).SingleOrDefault();
        }

        public static string GetNameAndAssemblyName(this Type type)
        {
            Check.ArgumentNotNull(type, nameof(type));
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        public static IEnumerable<MemberInfo> GetFieldsAndProperties(this Type type, BindingFlags bindingAttr)
        {
            foreach (var fi in type.GetFields(bindingAttr))
            {
                yield return fi;
            }

            foreach (var pi in type.GetProperties(bindingAttr))
            {
                yield return pi;
            }
        }

        public static MemberInfo GetFieldOrProperty(this Type type, string name, bool ignoreCase)
        {
            var flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;

            return type.GetSingleMember(
                name,
                MemberTypes.Field | MemberTypes.Property,
                flags);
        }

        public static List<MemberInfo> FindMembers(this Type targetType, MemberTypes memberType,
            BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
        {
            Check.ArgumentNotNull(targetType, nameof(targetType));

            var memberInfos =
                new List<MemberInfo>(targetType.FindMembers(memberType, bindingAttr, filter, filterCriteria));

            // fix weirdness with FieldInfos only being returned for the current Type
            // find base type fields and add them to result
            if ((memberType & MemberTypes.Field) != 0
                && (bindingAttr & BindingFlags.NonPublic) != 0)
            {
                // modify flags to not search for public fields
                var nonPublicBindingAttr = bindingAttr ^ BindingFlags.Public;

                while ((targetType = targetType.BaseType) != null)
                {
                    memberInfos.AddRange(targetType.FindMembers(MemberTypes.Field, nonPublicBindingAttr, filter,
                        filterCriteria));
                }
            }

            return memberInfos;
        }

        //public static Type MakeGenericType(this Type genericTypeDefinition, params Type[] innerTypes)
        //{
        //    Check.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
        //    Check.ArgumentNotEmpty<Type>(innerTypes, "innerTypes");
        //    Check.Argument.IsTrue(genericTypeDefinition.IsGenericTypeDefinition, "genericTypeDefinition", "Type '{0}' must be a generic type definition.".FormatInvariant(genericTypeDefinition));

        //    return genericTypeDefinition.MakeGenericType(innerTypes);
        //}

        public static object CreateGeneric(this Type genericTypeDefinition, Type innerType, params object[] args)
        {
            return CreateGeneric(genericTypeDefinition, new[] {innerType}, args);
        }

        public static object CreateGeneric(this Type genericTypeDefinition, Type[] innerTypes, params object[] args)
        {
            return CreateGeneric(genericTypeDefinition, innerTypes, (t, a) => Activator.CreateInstance(t, args));
        }

        public static object CreateGeneric(this Type genericTypeDefinition, Type[] innerTypes,
            Func<Type, object[], object> instanceCreator, params object[] args)
        {
            Check.ArgumentNotNull(() => genericTypeDefinition);
            Check.ArgumentNotNull(() => innerTypes);
            Check.ArgumentNotNull(() => instanceCreator);
            if (innerTypes.Length == 0)
                throw Error.Argument(nameof(innerTypes), "The sequence must contain at least one entry.");

            var specificType = genericTypeDefinition.MakeGenericType(innerTypes);

            return instanceCreator(specificType, args);
        }

        public static IList CreateGenericList(this Type listType)
        {
            Check.ArgumentNotNull(listType, nameof(listType));
            return (IList) typeof(List<>).CreateGeneric(listType);
        }

        //public static Type RemoveNullable(this Type type)
        //{
        //    if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
        //    {
        //        return type.GetGenericArguments()[0];
        //    }
        //    return type;
        //}

        public static bool IsEnumerable(this Type type)
        {
            Check.ArgumentNotNull(type, nameof(type));
            return type.IsAssignableFrom(typeof(IEnumerable));
        }

        public static bool IsGenericDictionary(this Type type)
        {
            if (type.IsInterface && type.IsGenericType)
            {
                return typeof(IDictionary<,>).Equals(type.GetGenericTypeDefinition());
            }
            return type.GetInterface(typeof(IDictionary<,>).Name) != null;
        }

        //public static bool IsListType(this Type type)
        //{
        //    Check.ArgumentNotNull(type, "type");

        //    if (type.IsArray)
        //        return true;
        //    else if (typeof(IList).IsAssignableFrom(type))
        //        return true;
        //    else if (type.IsSubClass(typeof(IList<>)))
        //        return true;
        //    else
        //        return false;
        //}

        /// <summary>
        ///     Gets the member's value on the object.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="target">The target object.</param>
        /// <returns>The member's value on the object.</returns>
        public static object GetValue(this MemberInfo member, object target)
        {
            Check.ArgumentNotNull(member, nameof(member));
            Check.ArgumentNotNull(target, nameof(target));

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return target.GetFieldValue(member.Name);
                //return ((FieldInfo)member).GetValue(target);
                case MemberTypes.Property:
                    return target.GetPropertyValue(member.Name);
                default:
                    throw new ArgumentException(
                        "MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatInvariant(member.Name),
                        nameof(member));
            }
        }

        /// <summary>
        ///     Sets the member's value on the target object.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public static void SetValue(this MemberInfo member, object target, object value)
        {
            Check.ArgumentNotNull(member, nameof(member));
            Check.ArgumentNotNull(target, nameof(target));

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    target.SetFieldValue(member.Name, value);
                    break;
                //return ((FieldInfo)member).GetValue(target);
                case MemberTypes.Property:
                    try
                    {
                        target.SetPropertyValue(member.Name, value);
                    }
                    catch (TargetParameterCountException e)
                    {
                        throw new ArgumentException(
                            "PropertyInfo '{0}' has index parameters".FormatInvariant(member.Name), nameof(member), e);
                    }
                    break;
                default:
                    throw new ArgumentException(
                        "MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatInvariant(member.Name),
                        nameof(member));
            }
        }

        /// <summary>
        ///     Gets the underlying type of a <see cref="Nullable{T}" /> type.
        /// </summary>
        public static Type GetNonNullableType(this Type type)
        {
            if (!IsNullable(type))
            {
                return type;
            }
            return type.GetGenericArguments()[0];
        }

        /// <summary>
        ///     Determines whether the specified MemberInfo can be read.
        /// </summary>
        /// <param name="member">The MemberInfo to determine whether can be read.</param>
        /// <returns>
        ///     <c>true</c> if the specified MemberInfo can be read; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///     For methods this will return <c>true</c> if the return type
        ///     is not <c>void</c> and the method is parameterless.
        /// </remarks>
        public static bool CanReadValue(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return true;
                case MemberTypes.Property:
                    return ((PropertyInfo) member).CanRead;
                case MemberTypes.Method:
                    var mi = (MethodInfo) member;
                    return mi.ReturnType != typeof(void) && mi.GetParameters().Length == 0;
                default:
                    return false;
            }
        }

        /// <summary>
        ///     Determines whether the specified MemberInfo can be set.
        /// </summary>
        /// <param name="member">The MemberInfo to determine whether can be set.</param>
        /// <returns>
        ///     <c>true</c> if the specified MemberInfo can be set; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanSetValue(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return true;
                case MemberTypes.Property:
                    return ((PropertyInfo) member).CanWrite;
                default:
                    return false;
            }
        }


        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider target, bool inherits)
            where TAttribute : Attribute
        {
            if (!target.IsDefined(typeof(TAttribute), inherits)) return null;

            var attributes = target.GetCustomAttributes(typeof(TAttribute), inherits);
            if (attributes.Length > 1)
            {
                throw Error.MoreThanOneElement();
            }

            return (TAttribute) attributes[0];
        }

        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider target, bool inherits)
            where TAttribute : Attribute
        {
            return target.IsDefined(typeof(TAttribute), inherits);
        }

        public static TAttribute[] GetAttributes<TAttribute>(this ICustomAttributeProvider target, bool inherits)
            where TAttribute : Attribute
        {
            if (!target.IsDefined(typeof(TAttribute), inherits)) return new TAttribute[0];

            var attributes = target
                .GetCustomAttributes(typeof(TAttribute), inherits)
                .Cast<TAttribute>();

            return SortAttributesIfPossible(attributes).ToArray();

            #region Obsolete

            //return target
            //    .GetCustomAttributes(typeof(TAttribute), inherits)
            //    .ToArray(a => (TAttribute)a);

            #endregion

            #region Obsolete

            //// OBSOLETE 1
            //return target.GetCustomAttributes(typeof(TAttribute), inherits).Cast<TAttribute>().ToArray();

            //// OBSOLETE 2
            //object[] attributesAsObjects = member.GetCustomAttributes(typeof(TAttribute), inherits);
            //TAttribute[] attributes = new TAttribute[attributesAsObjects.Length];
            //int index = 0;
            //Array.ForEach(attributesAsObjects,
            //    delegate(object o)
            //    {
            //        attributes[index++] = (TAttribute)o;
            //    });
            //return attributes;

            #endregion
        }

        /// <summary>
        ///     Given a particular MemberInfo, find all the attributes that apply to this
        ///     member. Specifically, it returns the attributes on the type, then (if it's a
        ///     property accessor) on the property, then on the member itself.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to retrieve.</typeparam>
        /// <param name="member">The member to look at.</param>
        /// <param name="inherits">true to include attributes inherited from base classes.</param>
        /// <returns>Array of found attributes.</returns>
        public static TAttribute[] GetAllAttributes<TAttribute>(this MemberInfo member, bool inherits)
            where TAttribute : Attribute
        {
            var attributes = new List<TAttribute>();

            if (member.DeclaringType != null)
            {
                attributes.AddRange(GetAttributes<TAttribute>(member.DeclaringType, inherits));

                var methodBase = member as MethodBase;
                if (methodBase != null)
                {
                    var prop = GetPropertyFromMethod(methodBase);
                    if (prop != null)
                    {
                        attributes.AddRange(GetAttributes<TAttribute>(prop, inherits));
                    }
                }
            }
            attributes.AddRange(GetAttributes<TAttribute>(member, inherits));
            return attributes.ToArray();
        }

        internal static IEnumerable<TAttribute> SortAttributesIfPossible<TAttribute>(IEnumerable<TAttribute> attributes)
            where TAttribute : Attribute
        {
            return typeof(IOrdered).IsAssignableFrom(typeof(TAttribute))
                ? attributes.Cast<IOrdered>().OrderBy(x => x.Ordinal).Cast<TAttribute>()
                : attributes;
        }

        /// <summary>
        ///     Given a MethodBase for a property's get or set method,
        ///     return the corresponding property info.
        /// </summary>
        /// <param name="method">MethodBase for the property's get or set method.</param>
        /// <returns>PropertyInfo for the property, or null if method is not part of a property.</returns>
        public static PropertyInfo GetPropertyFromMethod(this MethodBase method)
        {
            Check.ArgumentNotNull(method, nameof(method));

            PropertyInfo property = null;
            if (method.IsSpecialName)
            {
                var containingType = method.DeclaringType;
                if (containingType != null)
                {
                    if (method.Name.StartsWith("get_", StringComparison.InvariantCulture) ||
                        method.Name.StartsWith("set_", StringComparison.InvariantCulture))
                    {
                        var propertyName = method.Name.Substring(4);
                        property = containingType.GetProperty(propertyName);
                    }
                }
            }
            return property;
        }

        internal static Type FindIEnumerable(this Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (var arg in seqType.GetGenericArguments())
                {
                    var ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                        return ienum;
                }
            }
            var ifaces = seqType.GetInterfaces();
            if (ifaces.Length > 0)
            {
                foreach (var iface in ifaces)
                {
                    var ienum = FindIEnumerable(iface);
                    if (ienum != null)
                        return ienum;
                }
            }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
                return FindIEnumerable(seqType.BaseType);
            return null;
        }
    }
}