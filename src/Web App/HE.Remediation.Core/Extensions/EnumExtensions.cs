using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HE.Remediation.Core.Enums;

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

    public static string GetFilterName(this EApplicationStage applicationStage)
    {
        switch (applicationStage)
        {
            case EApplicationStage.ApplyForGrant:
                return "Apply for grant/Create building record";
            case EApplicationStage.GrantFundingAgreement:
                return "Grant Funding Agreement";
            case EApplicationStage.WorksPackage:
                return "Works Package";
            case EApplicationStage.WorksDelivery:
                return "Works Delivery";
            case EApplicationStage.WorksCompleted:
                return "Works Completed";
            case EApplicationStage.BuildingComplete:
                return "Building Complete";
            case EApplicationStage.Closed:
                return "Closed";
            default: return string.Empty;
        }
    }
}
