using FluentValidation;

namespace HE.Remediation.WebApp.CustomPropertyValidators;

public static partial class CustomPropertyValidators
{
    public static IRuleBuilderOptions<T, string> NotValidTelephoneGB<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Matches(@"^(?:0|\+?44)(?:\d\s?){9,10}$")
            .WithMessage("Enter a telephone number, like 01632 960 001, 07700 900 982");
    }

    public static IRuleBuilderOptions<T, string> NotValidTelephoneInternational<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Matches(@"^(\+?[0-9]*)$")
            .WithMessage("Enter a telephone number, like +331632960001, +497700900982");
    }
}