using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Decision.Common.Extensions;
using Decision.Common.Infrastructure;

namespace Decision.Common.GuardToolkit
{
    public static class Check
    {
        #region Constants
        private const string AgainstMessage = "Assertion evaluation failed with 'false'.";
        private const string ImplementsMessage = "Type '{0}' must implement type '{1}'.";
        private const string InheritsFromMessage = "Type '{0}' must inherit from type '{1}'.";
        private const string IsTypeOfMessage = "Type '{0}' must be of type '{1}'.";
        private const string IsEqualMessage = "Compared objects must be equal.";
        private const string IsPositiveMessage = "Argument '{0}' must be a positive value. Value: '{1}'.";
        private const string IsTrueMessage = "True expected for '{0}' but the condition was False.";
        private const string NotNegativeMessage = "Argument '{0}' cannot be a negative value. Value: '{1}'.";

        #endregion

        #region Public Methods

        /// <summary>
        ///     Throws proper exception if the class reference is null.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value">Class reference to check.</param>
        /// <exception cref="InvalidOperationException">If class reference is null.</exception>
        [DebuggerStepThrough]
        public static void NotNull<TValue>(Func<TValue> value)
        {
            var handler = value;
            if (handler == null) return;
            if (handler() == null)
                throw new InvalidOperationException("'{0}' cannot be null.".FormatInvariant(value));
        }

        [DebuggerStepThrough]
        public static void ArgumentNotNull(object arg, string argName)
        {
            if (arg == null)
                Error.ArgumentNull(argName);
        }

        [DebuggerStepThrough]
        public static void ArgumentNotNull<T>(Func<T> arg)
        {
            var handler = arg;
            ArgumentNotNull(handler, nameof(arg));

            ArgumentNotNull(handler, GetParamName(handler));
        }

        [DebuggerStepThrough]
        public static void Arguments<T1, T2>(Func<T1> arg1, Func<T2> arg2)
        {
            ArgumentNotNull(arg1);
            ArgumentNotNull(arg2);
        }

        [DebuggerStepThrough]
        public static void Arguments<T1, T2, T3>(Func<T1> arg1, Func<T2> arg2, Func<T3> arg3)
        {
            ArgumentNotNull(arg1);
            ArgumentNotNull(arg2);
            ArgumentNotNull(arg3);
        }

        [DebuggerStepThrough]
        public static void Arguments<T1, T2, T3, T4>(Func<T1> arg1, Func<T2> arg2, Func<T3> arg3, Func<T4> arg4)
        {
            ArgumentNotNull(arg1);
            ArgumentNotNull(arg2);
            ArgumentNotNull(arg3);
            ArgumentNotNull(arg4);
        }

        [DebuggerStepThrough]
        public static void Arguments<T1, T2, T3, T4, T5>(Func<T1> arg1, Func<T2> arg2, Func<T3> arg3, Func<T4> arg4,
            Func<T5> arg5)
        {
            ArgumentNotNull(arg1);
            ArgumentNotNull(arg2);
            ArgumentNotNull(arg3);
            ArgumentNotNull(arg4);
            ArgumentNotNull(arg5);
        }

        [DebuggerStepThrough]
        public static void ArgumentNotEmpty(Func<string> arg)
        {
            var handler = arg;
            if (handler == null) return;
            if (!handler().IsEmpty()) return;

            var argName = GetParamName(handler);
            throw Error.Argument(argName, "String parameter '{0}' cannot be null or all whitespace.", argName);
        }

        [DebuggerStepThrough]
        public static void ArgumentNotEmpty(Func<Guid> arg)
        {
            if (arg == null || arg() != Guid.Empty) return;

            var argName = GetParamName(arg);
            throw Error.Argument(argName, "Argument '{0}' cannot be an empty guid.", argName);
        }

      
        [DebuggerStepThrough]
        public static void ArgumentNotEmpty<T>(ICollection<T> arg, string argName)
        {
            if (arg != null && !arg.Any())
                throw Error.Argument(argName, "Collection cannot be null and must have at least one item.");
        }

        [DebuggerStepThrough]
        public static void ArgumentNotEmpty(Guid arg, string argName)
        {
            if (arg == Guid.Empty)
                throw Error.Argument(argName, "Argument '{0}' cannot be an empty guid.", argName);
        }

        [DebuggerStepThrough]
        public static void ArgumentInRange<T>(T arg, T min, T max, string argName) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(min) < 0 || arg.CompareTo(max) > 0)
                throw Error.ArgumentOutOfRange(argName, "The argument '{0}' must be between '{1}' and '{2}'.", argName,
                    min, max);
        }

        [DebuggerStepThrough]
        public static void ArgumentNotOutOfLength(string arg, int maxLength, string argName)
        {
            if (arg.Trim().Length > maxLength)
            {
                throw Error.Argument(argName, "Argument '{0}' cannot be more than {1} characters long.", argName,
                    maxLength);
            }
        }

        [DebuggerStepThrough]
        public static void ArgumentNotNegative<T>(T arg, string argName, string message = NotNegativeMessage)
            where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) < 0)
                throw Error.ArgumentOutOfRange(argName, message.FormatInvariant(argName, arg));
        }

        [DebuggerStepThrough]
        public static void ArgumentNotZero<T>(T arg, string argName) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) == 0)
                throw Error.ArgumentOutOfRange(argName,
                    "Argument '{0}' must be greater or less than zero. Value: '{1}'.", argName, arg);
        }

        [DebuggerStepThrough]
        public static void Against<TException>(bool assertion, string message = AgainstMessage)
            where TException : Exception
        {
            if (assertion)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerStepThrough]
        public static void Against<TException>(Func<bool> assertion, string message = AgainstMessage)
            where TException : Exception
        {
            //Execute the lambda and if it evaluates to true then throw the exception.
            if (assertion())
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerStepThrough]
        public static void InheritsFrom<TBase>(Type type)
        {
            InheritsFrom<TBase>(type, InheritsFromMessage.FormatInvariant(type.FullName, typeof(TBase).FullName));
        }

        [DebuggerStepThrough]
        public static void InheritsFrom<TBase>(Type type, string message)
        {
            if (type.BaseType != typeof(TBase))
                throw new InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        public static void Implements<TInterface>(Type type, string message = ImplementsMessage)
        {
            if (!typeof(TInterface).IsAssignableFrom(type))
                throw new InvalidOperationException(message.FormatInvariant(type.FullName, typeof(TInterface).FullName));
        }

        [DebuggerStepThrough]
        public static void IsSubclassOf<TBase>(Type type)
        {
            var baseType = typeof(TBase);
            if (!baseType.IsSubClass(type))
            {
                throw new InvalidOperationException(
                    "Type '{0}' must be a subclass of type '{1}'.".FormatInvariant(type.FullName, baseType.FullName));
            }
        }

        [DebuggerStepThrough]
        public static void IsTypeOf<TType>(object instance)
        {
            IsTypeOf<TType>(instance, IsTypeOfMessage.FormatInvariant(instance.GetType().Name, typeof(TType).FullName));
        }

        [DebuggerStepThrough]
        public static void IsTypeOf<TType>(object instance, string message)
        {
            if (!(instance is TType))
                throw new InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        public static void IsEqual<TException>(object compare, object instance, string message = IsEqualMessage)
            where TException : Exception
        {
            if (!compare.Equals(instance))
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerStepThrough]
        public static void HasDefaultConstructor<T>()
        {
            HasDefaultConstructor(typeof(T));
        }

        [DebuggerStepThrough]
        public static void HasDefaultConstructor(Type t)
        {
            if (!t.HasDefaultConstructor())
                throw Error.InvalidOperation("The type '{0}' must have a default parameterless constructor.", t.FullName);
        }

        [DebuggerStepThrough]
        public static void ArgumentIsPositive<T>(T arg, string argName, string message = IsPositiveMessage)
            where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) < 1)
                throw Error.ArgumentOutOfRange(argName, message.FormatInvariant(argName));
        }

        [DebuggerStepThrough]
        public static void ArgumentIsTrue(bool arg, string argName, string message = IsTrueMessage)
        {
            if (!arg)
                throw Error.Argument(argName, message.FormatInvariant(argName));
        }


        [DebuggerStepThrough]
        public static void ArgumentIsEnumType(Type arg, string argName)
        {
            ArgumentNotNull(arg, argName);
            if (!arg.IsEnum)
                throw Error.Argument(argName, "Type '{0}' must be a valid Enum type.", arg.FullName);
        }

        [DebuggerStepThrough]
        public static void ArgumentIsEnumType(Type enumType, object arg, string argName)
        {
            ArgumentNotNull(arg, argName);
            if (!Enum.IsDefined(enumType, arg))
            {
                throw Error.ArgumentOutOfRange(argName,
                    "The value of the argument '{0}' provided for the enumeration '{1}' is invalid.", argName,
                    enumType.FullName);
            }
        }


        [DebuggerStepThrough]
        public static void ArgumentNotDisposed(DisposableBase arg, string argName)
        {
            ArgumentNotNull(arg, argName);
            if (arg.IsDisposed)
                throw Error.ObjectDisposed(argName);
        }


        [DebuggerStepThrough]
        public static void PagingArgsValid(int indexArg, int sizeArg, string indexArgName, string sizeArgName)
        {
            ArgumentNotNegative(indexArg, indexArgName, "PageIndex cannot be below 0");
            if (indexArg > 0)
            {
                // if pageIndex is specified (> 0), PageSize CANNOT be 0 
                ArgumentIsPositive(sizeArg, sizeArgName,
                    "PageSize cannot be below 1 if a PageIndex greater 0 was provided.");
            }
            else
            {
                // pageIndex 0 actually means: take all!
                ArgumentNotNegative(sizeArg, sizeArgName);
            }
        }

        [DebuggerStepThrough]
        private static string GetParamName<T>(Expression<Func<T>> expression)
        {
            var name = string.Empty;
            var body = expression.Body as MemberExpression;

            if (body != null)
            {
                name = body.Member.Name;
            }

            return name;
        }

        [DebuggerStepThrough]
        private static string GetParamName<T>(Func<T> expression)
        {
            return expression.Method.Name;
        }

        [DebuggerStepThrough]
        public static T ArgumentNotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                Error.ArgumentNull(parameterName);
            return value;
        }

        [DebuggerStepThrough]
        public static T? ArgumentNotNull<T>(T? value, string parameterName) where T : struct
        {
            if (!value.HasValue)
                throw new ArgumentNullException(parameterName);
            return value;
        }

        [DebuggerStepThrough]
        public static string ArgumentNotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(parameterName));
            return value;
        }

        //public static bool ContainsNumber(this string inputText)
        //{
        //    return !string.IsNullOrWhiteSpace(inputText) && inputText.ToEnglishNumbers().Any(char.IsDigit);
        //}

        [DebuggerStepThrough]
        public static bool HasConsecutiveChars(string inputText, int sequenceLength = 3)
        {
            var charEnumerator = StringInfo.GetTextElementEnumerator(inputText);
            var currentElement = string.Empty;
            var count = 1;
            while (charEnumerator.MoveNext())
            {
                if (currentElement == charEnumerator.GetTextElement())
                {
                    if (++count >= sequenceLength)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                    currentElement = charEnumerator.GetTextElement();
                }
            }
            return false;
        }

        [DebuggerStepThrough]
        public static bool IsEmailAddress(string inputText)
        {
            return !string.IsNullOrWhiteSpace(inputText) && new EmailAddressAttribute().IsValid(inputText);
        }

        //public static bool IsNumeric(this string inputText)
        //{
        //    if (string.IsNullOrWhiteSpace(inputText)) return false;

        //    long inputNumber;
        //    return long.TryParse(inputText.ToEnglishNumbers(), out inputNumber);
        //}
        #endregion
    }
}
