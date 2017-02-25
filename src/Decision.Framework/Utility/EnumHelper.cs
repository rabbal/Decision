using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Decision.Framework.Utility
{
    public static class EnumHelper
    {
        public static IEnumerable<SelectListItem> GetItems(
            this Type enumType, int? selectedValue)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
            {
                throw new ArgumentException("Type must be an enum");
            }

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();

            var items = names.Zip(values, (name, value) =>
                new SelectListItem
                {
                    Text = GetName(enumType, name),
                    Value = value.ToString(CultureInfo.InvariantCulture),
                    Selected = value == selectedValue
                }
                );
            return items;
        }

        private static string GetName(Type enumType, string name)
        {
            var result = name;

            var attribute = enumType
                .GetField(name)
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }

            return result;
        }

        public static string GetEnumDescription(Type type, string value)
        {
            var name =
                Enum.GetNames(type)
                    .Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                    .Select(d => d)
                    .FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute) customAttribute[0]).Description : name;
        }
    }
}