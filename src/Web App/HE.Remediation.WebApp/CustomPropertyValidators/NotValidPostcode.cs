using FluentValidation;

namespace HE.Remediation.WebApp.CustomPropertyValidators;

public static partial class CustomPropertyValidators
{
    public static IRuleBuilderOptions<T, string> NotValidPostcode<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Matches(@"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$")
            .WithMessage("Please enter a valid UK postcode")
            .Must(ValidUkBasedPostcode.BeAUkBasedPostcode)
            .WithMessage("The post code must be UK based");
    }
}