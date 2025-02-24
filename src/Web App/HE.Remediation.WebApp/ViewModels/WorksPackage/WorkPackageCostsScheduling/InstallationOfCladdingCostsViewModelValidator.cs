using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class InstallationOfCladdingCostsViewModelValidator : AbstractValidator<InstallationOfCladdingCostsViewModel>
{
    public InstallationOfCladdingCostsViewModelValidator()
    {
        RuleFor(x => x.NewCladdingAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("New cladding amount must be a number")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.NewCladdingDescription), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter new cladding amount")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"New cladding amount must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.NewCladdingAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.NewCladdingAmountText))
                    .WithMessage(x => "New cladding amount must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.NewCladdingAmountText))
                    .WithMessage(x => "New cladding amount must be a whole number");
            });

        RuleFor(x => x.NewCladdingDescription)
            .MaximumLength(500)
            .WithMessage("Information about costs of new cladding cannot exceed 500 characters")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.NewCladdingAmountText), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter information about costs of new cladding");

        RuleFor(x => x.ExternalWorksAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("External wall system amount must be a number")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.ExternalWorksDescription), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter external wall system amount")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"External wall system amount must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.ExternalWorksAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.ExternalWorksAmountText))
                    .WithMessage(x => "External wall system amount must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.ExternalWorksAmountText))
                    .WithMessage(x => "External wall system amount must be a whole number");
            });

        RuleFor(x => x.ExternalWorksDescription)
            .MaximumLength(500)
            .WithMessage("Information about costs of the external wall system cannot exceed 500 characters")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.ExternalWorksAmountText), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter information about costs of external wall system");

        RuleFor(x => x.InternalWorksAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Internal mitigation works amount must be a number")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.InternalWorksDescription), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter internal mitigation works amount")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Internal mitigation works amount must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.InternalWorksAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.InternalWorksAmountText))
                    .WithMessage(x => "Internal mitigation works amount must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.InternalWorksAmountText))
                    .WithMessage(x => "Internal mitigation works amount must be a whole number");
            });

        RuleFor(x => x.InternalWorksDescription)
            .MaximumLength(500)
            .WithMessage("Information about costs of internal mitigation works cannot exceed 500 characters")
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.InternalWorksAmountText), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter information about internal mitigation works");
    }
}