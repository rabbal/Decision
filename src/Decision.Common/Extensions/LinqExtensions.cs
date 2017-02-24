using System;
using System.Linq.Expressions;
using System.Reflection;
using NTierMvcFramework.Common.Infrastructure;

namespace NTierMvcFramework.Common.Extensions
{
    public static class LinqExtensions
    {
        public static PropertyInfo ExtractPropertyInfo(this LambdaExpression propertyAccessor)
        {
            return propertyAccessor.ExtractMemberInfo() as PropertyInfo;
        }

        public static FieldInfo ExtractFieldInfo(this LambdaExpression propertyAccessor)
        {
            return propertyAccessor.ExtractMemberInfo() as FieldInfo;
        }

        public static MemberInfo ExtractMemberInfo(this LambdaExpression propertyAccessor)
        {
            Guard.ArgumentNotNull(() => propertyAccessor);

            MemberInfo info;
            try
            {
                MemberExpression operand;
                // o => o.PropertyOrField
                var expression = propertyAccessor;

                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    // If the property is not an Object, then the member access expression will be wrapped in a conversion expression
                    // (object)o.PropertyOrField
                    var body = unaryExpression;
                    // o.PropertyOrField
                    operand = (MemberExpression) body.Operand;
                }
                else
                {
                    // o.PropertyOrField
                    operand = (MemberExpression) expression.Body;
                }

                // Member
                var member = operand.Member;
                info = member;
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "The property or field accessor expression is not in the expected format 'o => o.PropertyOrField'.",
                    e);
            }

            return info;
        }
    }
}