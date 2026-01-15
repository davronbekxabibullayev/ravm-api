namespace Devhub.Common.Paging.Utils;

using System.Reflection;

internal static class TypeUtils
{
    public static PropertyInfo? GetProperty(Type type, string propertyName)
    {
        var parts = propertyName.Split('.');

        if (parts.Length > 1)
        {
            var nestedProperty = type.GetProperty(parts[0])
                ?? throw new InvalidOperationException($"Property ${parts[0]} not does not exist.");

            return GetProperty(nestedProperty.PropertyType, parts.Skip(1).Aggregate((a, i) => a + "." + i));
        }

        return type.GetProperty(propertyName);
    }
}
