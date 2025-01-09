using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class IneligibleCostsChangesViewModelValidator : AbstractValidator<IneligibleCostsChangesViewModel>
{
    public IneligibleCostsChangesViewModelValidator()
    {
        RuleFor(x => x.VariationIneligibleAmountText)
            .Must(x => decimal.TryParse(x, out _))
                .When(x => !string.IsNullOrWhiteSpace(x.VariationIneligibleAmountText))
                .WithMessage("Ineligible costs must be a number")
            .Must(x => x.NotExceedMaximumDigits())
                .WithMessage(x => $"Ineligible costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.VariationIneligibleAmount)
                .NotEmpty()
                    .WithMessage("Enter ineligible costs")
                .GreaterThanOrEqualTo(1M)
                        .OverridePropertyName(nameof(IneligibleCostsChangesViewModel.VariationIneligibleAmountText))
                        .WithMessage(x => "Ineligible costs must be more than zero")
                    .Must(x => x.HaveNoDecimalsInAmount())
                        .OverridePropertyName(nameof(IneligibleCostsChangesViewModel.VariationIneligibleAmountText))
                        .WithMessage(x => "Ineligible costs must be a whole number");
            });

        RuleFor(x => x.VariationIneligibleDescription)
            .Empty()
                .When(x => x.VariationIneligibleAmount is null &&
                    string.IsNullOrEmpty(x.VariationIneligibleAmountText))
                .WithMessage("Remove information about the changes to Ineligible costs when no change to cost is entered")
            .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.VariationIneligibleAmountText) & x.VariationIneligibleAmount != 0, ApplyConditionTo.CurrentValidator)
                .WithMessage("Enter information about the ineligible costs")
            .MaximumLength(500)
                .WithMessage("Information about the ineligible costs cannot exceed 500 characters");
    }
}
