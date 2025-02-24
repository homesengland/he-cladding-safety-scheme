using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class OtherCostsViewModelValidator : AbstractValidator<OtherCostsViewModel>
{
    public OtherCostsViewModelValidator()
    {
        RuleFor(x => x.FraewSurveyAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("FRAEW survey amount must be a number")
            .NotEmpty()
            .WithMessage("Enter FRAEW survey amount")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"FRAEW survey amount must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.FraewSurveyAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(OtherCostsViewModel.FraewSurveyAmountText))
                    .WithMessage(x => "FRAEW survey amount must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(OtherCostsViewModel.FraewSurveyAmountText))
                    .WithMessage(x => "FRAEW survey amount must be a whole number");
            });

        RuleFor(x => x.FeasibilityStageAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Feasibility stage costs must be a number")
            .NotEmpty()
            .WithMessage("Enter feasibility stage costs")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Feasibility stage costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.FeasibilityStageAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(OtherCostsViewModel.FeasibilityStageAmountText))
                    .WithMessage(x => "Feasibility stage costs must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(OtherCostsViewModel.FeasibilityStageAmountText))
                    .WithMessage(x => "Feasibility stage costs must be a whole number");
            });

        RuleFor(x => x.FeasibilityStageDescription)
            .NotEmpty()
            .WithMessage("Enter information about the feasibility stage costs")
            .MaximumLength(500)
            .WithMessage("Information about costs of the feasibility stage cannot exceed 500 characters");

        RuleFor(x => x.PostTenderStageAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Post tender stage costs must be a number")
            .NotEmpty()
            .WithMessage("Enter post tender stage costs")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Post tender stage costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.PostTenderStageAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(OtherCostsViewModel.PostTenderStageAmountText))
                    .WithMessage(x => $"Post tender stage costs must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(OtherCostsViewModel.PostTenderStageAmountText))
                    .WithMessage(x => "Post tender stage costs must be a whole number");
            });

        RuleFor(x => x.PostTenderStageDescription)
            .NotEmpty()
            .WithMessage("Enter information about the post tender stage costs")
            .MaximumLength(500)
            .WithMessage("Information about costs of the post tender stage cannot exceed 500 characters");

        RuleFor(x => x.PropertyManagerAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Property manager/leaseholder liaison costs must be a number")
            .NotEmpty()
            .WithMessage("Enter property manager/leaseholder liaison costs")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Property manager/leaseholder liaison costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.PropertyManagerAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(OtherCostsViewModel.PropertyManagerAmountText))
                    .WithMessage(x => $"Property manager/leaseholder liaison costs must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(OtherCostsViewModel.PropertyManagerAmountText))
                    .WithMessage(x => "Property manager/leaseholder liaison costs must be a whole number");
            });

        RuleFor(x => x.PropertyManagerDescription)
            .NotEmpty()
            .WithMessage("Enter information about property manager/leaseholder liaison costs")
            .MaximumLength(500)
            .WithMessage("Information about property manager/leaseholder liaison costs cannot exceed 500 characters");

        RuleFor(x => x.IrrecoverableVatAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Irrecoverable VAT costs must be a number")
            .NotEmpty()
            .WithMessage("Enter irrecoverable VAT costs")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Irrecoverable VAT costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.IrrecoverableVatAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(OtherCostsViewModel.IrrecoverableVatAmountText))
                    .WithMessage(x => $"Irrecoverable VAT costs must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(OtherCostsViewModel.IrrecoverableVatAmountText))
                    .WithMessage(x => "Irrecoverable VAT costs must be a whole number");
            });

        RuleFor(x => x.IrrecoverableVatDescription)
            .NotEmpty()
            .WithMessage("Enter information about irrecoverable VAT costs")
            .MaximumLength(500)
            .WithMessage("Information about irrecoverable VAT costs cannot exceed 500 characters");
    }
}