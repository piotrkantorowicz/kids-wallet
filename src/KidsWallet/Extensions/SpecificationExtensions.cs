﻿using System.Reflection;
using System.Text;

namespace KidsWallet.Extensions;

public static class SpecificationExtensions
{
    public static string Print<T>(this T specification) where T : class
    {
        StringBuilder properties = new();
        PropertyInfo[] propertiesInfo = typeof(T).GetProperties();

        foreach (PropertyInfo property in propertiesInfo)
        {
            object? value = property.GetValue(obj: specification);

            if (value == null)
            {
                continue;
            }

            if (properties.Length > 0)
            {
                properties.Append(value: " and ");
            }

            properties.Append(handler: $"{property.Name}: {value}");
        }

        return properties.ToString();
    }
}