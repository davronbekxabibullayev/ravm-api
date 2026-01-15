namespace Ravm.Api.Utils.OpenXml;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class OfficeUtils
{

    public static string GetProperyValue(Type type, object obj, string propertyName)
    {
        if (obj == null)
            return string.Empty;
        var index = propertyName.IndexOf(".");

        if (index == -1)
            if (propertyName.EndsWith("()"))
            {
                var methodName = propertyName.Substring(0, propertyName.Length - 2);
                var method = type.GetMethod(methodName);
                var result = method.Invoke(obj, null);

                return result == null ? string.Empty : result.ToString();
            }
            else
            {
                string format = null;
                var doublePointIndexOf = propertyName.IndexOf(':');
                PropertyInfo property = null;
                string error = null;

                if (doublePointIndexOf == -1 && TryGetProperty(type, propertyName, out property, ref error))
                {
                    var displayFormatAttr = (DisplayFormatAttribute[])property.GetCustomAttributes(typeof(DisplayFormatAttribute), false);
                    format = displayFormatAttr.Length > 0 ? displayFormatAttr[0].DataFormatString : Constants.DEFAULT_FORMAT;
                }
                else if (doublePointIndexOf != -1 && TryGetProperty(type, propertyName.Substring(0, doublePointIndexOf), out property, ref error))
                    format = string.Format(Constants.CUSTOM_FORMAT, propertyName.Substring(doublePointIndexOf, propertyName.Length - doublePointIndexOf));

                return error == null ? string.Format(format, property.GetValue(obj, null) ?? string.Empty) : error;
            }
        else
        {
            var prop = type.GetProperty(propertyName.Substring(0, index));
            var innerPropertyName = propertyName.Substring(index + 1, propertyName.Length - index - 1);

            return GetProperyValue(prop.PropertyType, prop.GetValue(obj, null), innerPropertyName);
        }
    }

    public static bool TryGetProperty(Type type, string propertyName, out PropertyInfo result, ref string errorMessage)
    {
        result = type.GetProperty(propertyName);

        if (result == null)
        {
            errorMessage = string.Format("Property \"{0}\" not found in type \"{1}\"", propertyName, type.Name);
            return false;
        }
        else if (result.GetCustomAttributes(typeof(IgnoreAttribute), false).Length > 0)
        {
            errorMessage = string.Format("\"{0}\" in type \"{1}\" is ignore property");
            return false;
        }

        return true;
    }

    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
        return type.GetProperties().Where(a => a.GetCustomAttributes(typeof(IgnoreAttribute), false).Length == 0);
    }

}

