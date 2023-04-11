using FluentValidation;

namespace HE.Remediation.WebApp.CustomPropertyValidators;

public static partial class CustomPropertyValidators
{
    public static IRuleBuilderOptions<T, string> NotValidPostcode<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Matches(@"^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$")
            .WithMessage("Postcode must be valid");
    }
}