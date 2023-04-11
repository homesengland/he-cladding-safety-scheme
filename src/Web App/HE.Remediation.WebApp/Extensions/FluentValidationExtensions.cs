using FluentValidation;
using System.Text.RegularExpressions;

namespace HE.Remediation.WebApp.Extensions;

public static class FluentValidationExtensions
{
    private static readonly Regex numberValidation = new Regex(@"^\+?[0-9\s]+$", RegexOptions.Compiled);

    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(numberValidation)
            .WithMessage("{PropertyName} must be a valid phone number");
    }
}