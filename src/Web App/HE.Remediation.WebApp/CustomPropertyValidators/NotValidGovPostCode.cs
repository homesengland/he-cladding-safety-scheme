
using FluentValidation;

namespace HE.Remediation.WebApp.CustomPropertyValidators;

public static partial class CustomPropertyValidators
{
    /// <summary>
    /// Uses the government rule for a valid Post code. It also 
    /// outputs the appropriate message in here to reduce code duplication elsewhere 
    /// (we expect all validation messages to be consistent)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rule"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> ValidGovPostcode<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Matches(@"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$")
                   .WithMessage("Please enter a valid UK postcode");
    }
}


