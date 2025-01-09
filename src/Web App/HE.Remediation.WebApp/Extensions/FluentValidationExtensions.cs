using FluentValidation;
using System.Text.RegularExpressions;

namespace HE.Remediation.WebApp.Extensions;

public static class FluentValidationExtensions
{
    public const int MaxNumberOfDigitsInNumber = 10;

    private static readonly Regex numberValidation = new Regex(@"^\+?[0-9\s]+$", RegexOptions.Compiled);

    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(numberValidation)
            .WithMessage("{PropertyName} must be a valid phone number");
    }

    public static bool HaveNoDecimalsInAmount(this decimal? amount)
    {
        return amount.HasValue
            ? Math.Truncate(amount.Value) == amount.Value
            : true;
    }

    public static bool NotExceedMaximumDigits(this string input, int maxNumberOfDigits = MaxNumberOfDigitsInNumber)
    {
        if (input is null || string.IsNullOrWhiteSpace(input)) return true;

        var decimalPosition = input.IndexOf(".");
        var str = decimalPosition >= 0 
            ? input.Substring(0, decimalPosition) 
            : input;

        return new string(str.Where(char.IsDigit).ToArray()).Length <= maxNumberOfDigits;
    }
}