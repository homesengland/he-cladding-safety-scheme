using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class CheckYourAnswersViewModelValidator : AbstractValidator<CheckYourAnswersViewModel>
{
    public CheckYourAnswersViewModelValidator()
    {
        RuleFor(x => x.Defects)
            .NotEmpty()
            .When(x => x.HasFra == true && x.HasInternalFireSafetyRisks == true)
            .WithMessage("Enter at least one defect");

        RuleFor(x => x.FraFile)
            .NotEmpty()
            .When(x => x.HasFra == true)
            .WithMessage("Upload a file");
    }
}