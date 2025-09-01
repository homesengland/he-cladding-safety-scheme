using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class CheckYourAnswersViewModelValidator : AbstractValidator<CheckYourAnswersViewModel>
{
    public CheckYourAnswersViewModelValidator()
    {
        RuleFor(x => x.Defects)
            .NotEmpty()
            .When(x => x.HasInternalFireSafetyRisks == true)
            .WithMessage("Enter at least one defect");

        RuleFor(x => x.FraFile)
            .NotEmpty()
            .WithMessage("Upload a file");
    }
}