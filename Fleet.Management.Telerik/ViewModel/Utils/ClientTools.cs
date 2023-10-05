using System.Reflection;

namespace Fleet.Management.ViewModel.Utils;

public static class ClientTools
{
    /// <summary>
    /// Gets all enum values.
    /// </summary>
    /// <param name="enumType">Type of the enum.</param>
    /// <returns>Object array of all enum values</returns>
    public static object[] GetEnumValues(Type enumType)
    {
        List<object> values = new();

        IEnumerable<FieldInfo> fields = from field in enumType.GetFields()
                                        where field.IsLiteral
                                        select field;

        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(enumType);
            values.Add(value);
        }

        return values.ToArray();
    }

    public static bool CheckProperties<TIn>(TIn input, ICollection<string> includedProperties = null, bool exclude = false)
        where TIn : class
    {
        if (input == null) return false;
        Type inType = input.GetType();

        bool result = true;

        foreach (PropertyInfo info in inType.GetProperties())
        {
            if ((((includedProperties != null) && (includedProperties.Contains(info.Name) != exclude)) || includedProperties == null)
                && info.CanRead && (info.GetValue(input) == null || string.IsNullOrWhiteSpace(info.GetValue(input).ToString())))
                result = false;
        }

        return result;
    }
}
