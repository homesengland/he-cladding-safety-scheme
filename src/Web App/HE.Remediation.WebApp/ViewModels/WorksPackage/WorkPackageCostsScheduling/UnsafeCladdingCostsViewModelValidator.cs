using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class UnsafeCladdingCostsViewModelValidator : AbstractValidator<UnsafeCladdingCostsViewModel>
{
    public UnsafeCladdingCostsViewModelValidator()
    {
        RuleFor(x => x.UnsafeCladdingRemovalAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Unsafe cladding system removal amount must be a number")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.UnsafeCladdingRemovalDescription), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter unsafe cladding system removal amount")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Unsafe cladding system removal amount must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.UnsafeCladdingRemovalAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(UnsafeCladdingCostsViewModel.UnsafeCladdingRemovalAmountText))
                    .WithMessage(x => "Unsafe cladding system removal amount must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(UnsafeCladdingCostsViewModel.UnsafeCladdingRemovalAmountText))
                    .WithMessage(x => "Unsafe cladding system removal amount must be a whole number");
            });

        RuleFor(x => x.UnsafeCladdingRemovalDescription)
            .MaximumLength(500)
            .WithMessage("Information about the costs of unsafe cladding system removal cannot exceed 500 characters")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.UnsafeCladdingRemovalAmountText), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter information about the costs of unsafe cladding system removal");
    }
}