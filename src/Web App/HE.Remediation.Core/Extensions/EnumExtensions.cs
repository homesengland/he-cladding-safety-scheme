using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HE.Remediation.Core.Extensions;

public static class EnumExtensions
{
    public static TAttribute GetCustomAttribute<TAttribute>(this Enum value)
        where TAttribute : Attribute
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        return name != null ? type.GetField(name!)!
            .GetCustomAttribute<TAttribute>()
                : null;
    }

    public static string GetEnumDisplayName<T>(
        this T value) where T : struct, Enum
    {
        var displayAttribute = value.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute != null && !string.IsNullOrWhiteSpace(displayAttribute.Name)
            ? displayAttribute.Name
            : null;
    }
}
