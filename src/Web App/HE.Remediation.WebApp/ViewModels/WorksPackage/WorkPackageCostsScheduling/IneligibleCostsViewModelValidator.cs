using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class IneligibleCostsViewModelValidator : AbstractValidator<IneligibleCostsViewModel>
{
    public IneligibleCostsViewModelValidator()
    {
        RuleFor(x => x.IneligibleAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Ineligible costs must be a number")
            .NotEmpty()
            .WithMessage("Enter ineligible costs")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Ineligible costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.IneligibleAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(IneligibleCostsViewModel.IneligibleAmountText))
                    .WithMessage(x => "Ineligible costs must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(IneligibleCostsViewModel.IneligibleAmountText))
                    .WithMessage(x => "Ineligible costs must be a whole number");
            });

        RuleFor(x => x.IneligibleDescription)
            .NotEmpty()
            .WithMessage("Enter information about the ineligible costs")
            .MaximumLength(500)
            .WithMessage("Information about ineligible costs cannot exceed 500 characters");
    }
}