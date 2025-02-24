using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class InstallationOfCladdingCostsViewModelValidator : AbstractValidator<InstallationOfCladdingCostsViewModel>
    {
        public InstallationOfCladdingCostsViewModelValidator()
        {
            RuleFor(x => x.VariationNewCladdingAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationNewCladdingAmountText))
                    .WithMessage("New cladding additional cost must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                .WithMessage(x => $"New cladding additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationNewCladdingAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.VariationNewCladdingAmountText))
                            .WithMessage(x => "New cladding additional cost must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.VariationNewCladdingAmountText))
                            .WithMessage(x => "New cladding additional cost must be a whole number");
                });

            RuleFor(x => x.VariationNewCladdingDescription)
                .Empty()
                    .When(x => x.VariationNewCladdingAmount == 0 || x.VariationNewCladdingAmount is null &&
                        string.IsNullOrEmpty(x.VariationNewCladdingAmountText))
                    .WithMessage("Remove information about the changes to costs for the new cladding when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationNewCladdingAmountText) & x.VariationNewCladdingAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to costs for the new cladding")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to costs for the new cladding cannot exceed 500 characters");


            RuleFor(x => x.VariationOtherEligibleWorkToExternalWallAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationOtherEligibleWorkToExternalWallAmountText))
                    .WithMessage("Works to external wall system additional cost must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Works to external wall system additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationOtherEligibleWorkToExternalWallAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.VariationOtherEligibleWorkToExternalWallAmountText))
                            .WithMessage(x => "Works to external wall system additional cost must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.VariationOtherEligibleWorkToExternalWallAmountText))
                            .WithMessage(x => "Works to external wall system additional cost must be a whole number");
                });

            RuleFor(x => x.VariationOtherEligibleWorkToExternalWallDescription)
                .Empty()
                    .When(x => x.VariationOtherEligibleWorkToExternalWallAmount == 0 || x.VariationOtherEligibleWorkToExternalWallAmount is null &&
                        string.IsNullOrEmpty(x.VariationOtherEligibleWorkToExternalWallAmountText))
                    .WithMessage("Remove information about the changes to costs for the works to external wall system when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationOtherEligibleWorkToExternalWallAmountText) & x.VariationOtherEligibleWorkToExternalWallAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to costs for the works to external wall system")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to costs for the works to external wall system cannot exceed 500 characters");


            RuleFor(x => x.VariationInternalMitigationWorksAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationInternalMitigationWorksAmountText))
                    .WithMessage("Internal mitigation works additional cost must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Internal mitigation works additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationInternalMitigationWorksAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.VariationInternalMitigationWorksAmountText))
                            .WithMessage(x => "Internal mitigation works additional cost must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(InstallationOfCladdingCostsViewModel.VariationInternalMitigationWorksAmountText))
                            .WithMessage(x => "Internal mitigation works additional cost must be a whole number");
                });

            RuleFor(x => x.VariationInternalMitigationWorksDescription)
                .Empty()
                    .When(x => x.VariationInternalMitigationWorksAmount == 0 || x.VariationInternalMitigationWorksAmount is null &&
                        string.IsNullOrEmpty(x.VariationInternalMitigationWorksAmountText))
                    .WithMessage("Remove information about the changes to costs for the internal mitigation works when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationInternalMitigationWorksAmountText) & x.VariationInternalMitigationWorksAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to costs for the internal mitigation works")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to costs for the internal mitigation works cannot exceed 500 characters");
        }
    }
}
