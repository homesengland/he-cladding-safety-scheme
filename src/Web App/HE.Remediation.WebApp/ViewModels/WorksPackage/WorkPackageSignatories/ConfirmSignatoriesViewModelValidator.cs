using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageSignatories;

public class ConfirmSignatoriesViewModelValidator : AbstractValidator<ConfirmSignatoriesViewModel>
{
    public ConfirmSignatoriesViewModelValidator()
    {
        RuleFor(x => x.AreSignatoriesCorrect)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
