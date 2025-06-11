using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.BuildingsInsurance
{
    public class BuildingsInsuranceViewModelValidator : AbstractValidator<BuildingsInsuranceViewModel>
    {
        public BuildingsInsuranceViewModelValidator()
        {

            RuleFor(x => x.SumInsuredAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.SumInsuredAmountText))
                    .WithMessage("Sum insured must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Sum insured must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.SumInsuredAmount)
                        .NotEmpty()
                            .WithMessage("Enter the total sum insured of the building")
                        .GreaterThanOrEqualTo(1M)
                            .OverridePropertyName(nameof(BuildingsInsuranceViewModel.SumInsuredAmountText))
                            .WithMessage(x => "Sum insured must be greater than zero")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(BuildingsInsuranceViewModel.SumInsuredAmountText))
                            .WithMessage(x => "Sum insured must be a whole number");
                });

            RuleFor(x => x.CurrentBuildingInsurancePremiumAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.CurrentBuildingInsurancePremiumAmountText))
                    .WithMessage("Amount must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Amount must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.CurrentBuildingInsurancePremiumAmount)
                        .NotEmpty()
                            .WithMessage("Enter the current buildings insurance premium for the whole building")
                        .GreaterThanOrEqualTo(1M)
                            .OverridePropertyName(nameof(BuildingsInsuranceViewModel.CurrentBuildingInsurancePremiumAmountText))
                            .WithMessage(x => "Amount must be greater than zero")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(BuildingsInsuranceViewModel.CurrentBuildingInsurancePremiumAmountText))
                            .WithMessage(x => "Amount must be a whole number");
                });

            RuleFor(x => x.SelectedInsuranceProviderIds)
                .NotEmpty()
                .WithMessage("Select who is/are your current buildings insurance provider/s");

            RuleFor(x => x.IfOtherInsuranceProviderName)
                .NotEmpty()
                .When(x => x.SelectedInsuranceProviderIds.Contains(11) /* Other */, ApplyConditionTo.CurrentValidator)
                .WithMessage("If Other, please tell us who is/are your current building insurance provider/s?")
                .MaximumLength(150)
                .WithMessage("The maximum number of characters allowed is 150");

            RuleFor(x => x.AdditionalInfo)
                .MaximumLength(500)
                .WithMessage("The maximum number of characters allowed is 500");

        }
    }
}