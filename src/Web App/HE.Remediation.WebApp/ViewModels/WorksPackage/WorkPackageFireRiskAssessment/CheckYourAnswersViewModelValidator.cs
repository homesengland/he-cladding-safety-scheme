using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class CheckYourAnswersViewModelValidator : AbstractValidator<CheckYourAnswersViewModel>
{
    public CheckYourAnswersViewModelValidator()
    {
        When(x => x.HasInternalFireSafetyRisks == true, () =>
        {
            RuleFor(x => x.Defects)
                .NotEmpty()
                .WithMessage("Enter at least one defect");

            RuleFor(x => x.HasFunding)
                .NotNull()
                .WithMessage("State if you have funding for your identified defects");

            RuleFor(x => x.FraFundingType)
                .NotNull()
                .WithMessage("State the funding type you have or the action you will undertake");
        });

        RuleFor(x => x.FraFile)
            .NotEmpty()
            .WithMessage("Upload a file");
    }
}