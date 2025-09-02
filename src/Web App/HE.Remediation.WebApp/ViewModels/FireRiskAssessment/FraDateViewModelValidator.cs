using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FraDateViewModelValidator : AbstractValidator<FraDateViewModel>
{
    public FraDateViewModelValidator()
    {
        RuleFor(x => x.FraDate)
            .NotNull()
            .WithMessage("Enter a date")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Date entered must be in the past")
            .ValidDate();
    }
}