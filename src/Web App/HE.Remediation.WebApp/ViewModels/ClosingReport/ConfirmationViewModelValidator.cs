using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class ConfirmationViewModelValidator : AbstractValidator<ConfirmationViewModel>
{
    public ConfirmationViewModelValidator()
    {

        RuleFor(x => x.FinalCostReport)
            .Must(x => x == true)
            .WithMessage("You must confirm final cost report from your grant certifying officer");

        RuleFor(x => x.ExitFraew)
                        .Must(x => x == true)
                        .WithMessage("You must confirm exit FRAEW report and letter from fire risk assessor");

        RuleFor(x => x.CompletionCertificate)
                        .Must(x => x == true)
                        .WithMessage("You must confirm completion certificate from your Building Control Body");

        RuleFor(x => x.InformedPracticalCompletion)
                                .Must(x => x == true)
                                .WithMessage("You must confirm leaseholders and residents have been informed");      
    }
}
