
using FluentValidation;
using System.Text.RegularExpressions;

namespace HE.Remediation.WebApp.CustomPropertyValidators;

public static partial class CustomPropertyValidators
{
    private static readonly Regex _emailRegex = new Regex(
       @"^[^@\s]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$",
       RegexOptions.Compiled | RegexOptions.IgnoreCase
   );

    public static IRuleBuilderOptions<T, string> NotValidEmailAddress<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Must(x =>
                    !string.IsNullOrWhiteSpace(x) &&
                    !x.Contains(" ") &&
                    _emailRegex.IsMatch(x))
                   .WithMessage("Enter an Email address in the correct format, like name@example.com");
    }
}
