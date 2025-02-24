using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class UnsafeCladdingCostsViewModelValidator : AbstractValidator<UnsafeCladdingCostsViewModel>
    {
        public UnsafeCladdingCostsViewModelValidator()
        {
            RuleFor(x => x.VariationRemovalOfCladdingAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationRemovalOfCladdingAmountText))
                    .WithMessage("Removal of unsafe cladding additional cost must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Removal of unsafe cladding additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationRemovalOfCladdingAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(UnsafeCladdingCostsViewModel.VariationRemovalOfCladdingAmountText))
                            .WithMessage(x => "Removal of unsafe cladding additional cost must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(UnsafeCladdingCostsViewModel.VariationRemovalOfCladdingAmountText))
                            .WithMessage(x => "Removal of unsafe cladding additional cost must be a whole number");
                });

            RuleFor(x => x.VariationRemovalOfCladdingDescription)
                .Empty()
                    .When(x => x.VariationRemovalOfCladdingAmount == 0 ||
                        x.VariationRemovalOfCladdingAmount is null &&
                        string.IsNullOrEmpty(x.VariationRemovalOfCladdingAmountText))
                    .WithMessage("Remove information about the changes to costs for the removal of unsafe cladding when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationRemovalOfCladdingAmountText) & x.VariationRemovalOfCladdingAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to costs for the removal of unsafe cladding")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to costs for the removal of unsafe cladding cannot exceed 500 characters");
        }
    }
}
