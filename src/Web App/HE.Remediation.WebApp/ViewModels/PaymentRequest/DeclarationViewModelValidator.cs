using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class DeclarationViewModelValidator : AbstractValidator<DeclarationViewModel>
{
    public DeclarationViewModelValidator()
    {
        string outputErrorMsg = "You must confirm all declarations before being able to submit your payment request";

        RuleFor(x => x.AwareProcess)
            .Must(x => x == true)
            .WithMessage(outputErrorMsg)
            .DependentRules(() => 
            {
                RuleFor(x => x.AwareNoPrecedentForFuture)
                        .Must(x => x == true)
                        .WithMessage(outputErrorMsg)
                .DependentRules(() =>
                {
                    RuleFor(x => x.PredictionsAccurate)
                        .Must(x => x == true)
                        .WithMessage(outputErrorMsg);
                });
            });        
    }
}
