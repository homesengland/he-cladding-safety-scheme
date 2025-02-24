
using FluentValidation;

namespace HE.Remediation.WebApp.CustomPropertyValidators;

public static partial class CustomPropertyValidators
{
    public static IRuleBuilderOptions<T, string> NotValidEmailAddress<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Must(x => (x == null) ? false : !x.Contains(" "))
                   .WithMessage(@"Enter an Email address in the correct format, like name@example.com");                    
    }
}
