using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Decision.Framework.Infrastructure;

namespace Decision.Framework.Extensions
{
    public static class TypeDescriptorExtensions
    {
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomTypeDescriptor td)
            where TAttribute : Attribute
        {
            var attributes = td.GetAttributes().OfType<TAttribute>();
            return TypeExtensions.SortAttributesIfPossible(attributes);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this PropertyDescriptor pd)
            where TAttribute : Attribute
        {
            var attributes = pd.Attributes.OfType<TAttribute>();
            return TypeExtensions.SortAttributesIfPossible(attributes);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this PropertyDescriptor pd,
            Func<TAttribute, bool> predicate)
            where TAttribute : Attribute
        {
            Check.ArgumentNotNull(predicate, nameof(predicate));

            var attributes = pd.Attributes.OfType<TAttribute>().Where(predicate);
            return TypeExtensions.SortAttributesIfPossible(attributes);
        }

        public static PropertyDescriptor GetProperty(this ICustomTypeDescriptor td, string name)
        {
            Check.ArgumentNotEmpty(name, nameof(name));
            return td.GetProperties().Find(name, true);
            //.Cast<PropertyDescriptor>()
            //.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<PropertyDescriptor> GetPropertiesWith<TAttribute>(this ICustomTypeDescriptor td)
            where TAttribute : Attribute
        {
            return td.GetPropertiesWith<TAttribute>(x => true);
        }

        public static IEnumerable<PropertyDescriptor> GetPropertiesWith<TAttribute>(
            this ICustomTypeDescriptor td,
            Func<TAttribute, bool> predicate)
            where TAttribute : Attribute
        {
            Check.ArgumentNotNull(predicate, nameof(predicate));

            return td.GetProperties()
                .Cast<PropertyDescriptor>()
                .Where(p => p.GetAttributes<TAttribute>().Any(predicate));

            //Expression<Func<TAttribute, bool>> expression = (TAttribute a) => typeof(TAttribute).IsAssignableFrom(a.GetType());

            //return td.GetProperties()
            //        .Cast<PropertyDescriptor>()
            //        .Where(p => p.Attributes.Cast<TAttribute>()
            //            .Any(expression.And(predicate).Compile()));

            //var query = from p in this.EntityTypeDescriptor.GetProperties().Cast<PropertyDescriptor>()
            //            let attributes = p.Attributes.Cast<TAttribute>()
            //            where attributes.Any(a => typeof(TAttribute).IsAssignableFrom(a.GetType()))
            //            //where attributes.Any(a => typeof(TAttribute).IsAssignableFrom(a.GetType()))
            //            select p;

            //return query 
            //        .Select<PropertyDescriptor, EntityProperty>(p => new EntityProperty(p, this))
            //        .AsReadOnly();
        }
    }
}