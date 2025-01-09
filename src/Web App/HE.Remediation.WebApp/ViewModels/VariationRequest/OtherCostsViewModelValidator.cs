using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class OtherCostsViewModelValidator : AbstractValidator<OtherCostsViewModel>
    {
        public OtherCostsViewModelValidator()
        {
            RuleFor(x => x.VariationFraewSurveyCostsAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationFraewSurveyCostsAmountText))
                    .WithMessage("FRAEW survey additional costs must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Feasibility stage additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationFraewSurveyCostsAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationFraewSurveyCostsAmountText))
                            .WithMessage(x => "FRAEW survey additional costs must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationFraewSurveyCostsAmountText))
                            .WithMessage(x => "FRAEW survey additional costs must be a whole number");
                });

            RuleFor(x => x.VariationFraewSurveyCostsDescription)
                .Empty()
                    .When(x => x.VariationFraewSurveyCostsAmount == 0 || x.VariationFraewSurveyCostsAmount is null &&
                        string.IsNullOrEmpty(x.VariationFraewSurveyCostsAmountText))
                    .WithMessage("Remove information about the changes to FRAEW survey costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationFraewSurveyCostsAmountText) & x.VariationFraewSurveyCostsAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to FRAEW survey costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to FRAEW survey costs cannot exceed 500 characters");


            RuleFor(x => x.VariationFeasibilityStageAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationFeasibilityStageAmountText))
                    .WithMessage("Feasibility stage additional costs must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Feasibility stage additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationFeasibilityStageAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationFeasibilityStageAmountText))
                            .WithMessage(x => "Feasibility stage additional costs must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationFeasibilityStageAmountText))
                            .WithMessage(x => "Feasibility stage additional costs must be a whole number");
                });

            RuleFor(x => x.VariationFeasibilityStageDescription)
                .Empty()
                    .When(x => x.VariationFeasibilityStageAmount == 0 || x.VariationFeasibilityStageAmount is null &&
                        string.IsNullOrEmpty(x.VariationFeasibilityStageAmountText))
                    .WithMessage("Remove information about the changes to feasibility stage costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationFeasibilityStageAmountText) & x.VariationFeasibilityStageAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to feasibility stage costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to feasibility stage cost cannot exceed 500 characters");


            RuleFor(x => x.VariationPostTenderStageAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationPostTenderStageAmountText))
                    .WithMessage("Post tender stage additional costs must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Post tender stage additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationPostTenderStageAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationPostTenderStageAmountText))
                            .WithMessage(x => "Post tender stage additional costs must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationPostTenderStageAmountText))
                            .WithMessage(x => "Post tender stage additional costs must be a whole number");
                });

            RuleFor(x => x.VariationPostTenderStageDescription)
                .Empty()
                    .When(x => x.VariationPostTenderStageAmount == 0 || x.VariationPostTenderStageAmount is null &&
                        string.IsNullOrEmpty(x.VariationPostTenderStageAmountText))
                    .WithMessage("Remove information about the changes to post tender stage costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationPostTenderStageAmountText) & x.VariationPostTenderStageAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to post tender stage costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to post tender stage costs cannot exceed 500 characters");



            RuleFor(x => x.VariationIrrecoverableVatAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationIrrecoverableVatAmountText))
                    .WithMessage("Irrecoverable VAT additional costs must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Irrecoverable VAT additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationIrrecoverableVatAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationIrrecoverableVatAmountText))
                            .WithMessage(x => "Irrecoverable VAT additional costs must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationIrrecoverableVatAmountText))
                            .WithMessage(x => "Irrecoverable VAT additional costs must be a whole number");
                });

            RuleFor(x => x.VariationIrrecoverableVatDescription)
                .Empty()
                    .When(x => x.VariationIrrecoverableVatAmount == 0 || x.VariationIrrecoverableVatAmount is null &&
                        string.IsNullOrEmpty(x.VariationIrrecoverableVatAmountText))
                    .WithMessage("Remove information about the changes to post irrecoverable VAT costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationIrrecoverableVatAmountText) & x.VariationIrrecoverableVatAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to post irrecoverable VAT costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to irrecoverable VAT costs cannot exceed 500 characters");


            RuleFor(x => x.VariationPropertyManagerAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationPropertyManagerAmountText))
                    .WithMessage("Property manager additional costs must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Property manager additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationPropertyManagerAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationPropertyManagerAmountText))
                            .WithMessage(x => "Property manager additional costs must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(OtherCostsViewModel.VariationPropertyManagerAmountText))
                            .WithMessage(x => "Property manager additional costs must be a whole number");
                });

            RuleFor(x => x.VariationPropertyManagerDescription)
                .Empty()
                    .When(x => x.VariationPropertyManagerAmount == 0 || x.VariationPropertyManagerAmount is null &&
                        string.IsNullOrEmpty(x.VariationPropertyManagerAmountText))
                    .WithMessage("Remove information about the changes to post property manager costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationPropertyManagerAmountText) & x.VariationPropertyManagerAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to post property manager costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to property manager costs cannot exceed 500 characters");
        }
    }
}
