using FluentValidation;

namespace HE.Remediation.WebApp.CustomPropertyValidators;

public static partial class CustomPropertyValidators
{
    private static readonly DateTime MinDate = new DateTime(1753, 1, 1);

    public static IRuleBuilderOptions<T, DateTime?> ValidDate<T>(this IRuleBuilder<T, DateTime?> rule)
    {
        return rule.GreaterThanOrEqualTo(MinDate)
            .WithMessage("Enter a valid date");
    }
}