using FluentValidation.TestHelper;
using HE.Remediation.WebApp.ViewModels.BuildingsInsurance;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.Tests.Validators
{
    public class BuildingsInsuranceViewModelValidatorTests
    {
        private readonly BuildingsInsuranceViewModelValidator _validator;

        public BuildingsInsuranceViewModelValidatorTests()
        {
            _validator = new BuildingsInsuranceViewModelValidator();
        }

        [Fact]
        public void Should_Have_Error_When_SumInsuredAmountText_Is_Not_A_Number()
        {
            var model = new BuildingsInsuranceViewModel { SumInsuredAmountText = "abc" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.SumInsuredAmountText)
                  .WithErrorMessage("Sum insured must be a number");
        }

        [Fact]
        public void Should_Have_Error_When_SumInsuredAmountText_Exceeds_Maximum_Digits()
        {
            var model = new BuildingsInsuranceViewModel { SumInsuredAmountText = new string('1', 21) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.SumInsuredAmountText)
                  .WithErrorMessage($"Sum insured must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits");
        }

        [Fact]
        public void Should_Have_Error_When_CurrentBuildingInsurancePremiumAmountText_Is_Not_A_Number()
        {
            var model = new BuildingsInsuranceViewModel { CurrentBuildingInsurancePremiumAmountText = "xyz" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CurrentBuildingInsurancePremiumAmountText)
                  .WithErrorMessage("Amount must be a number");
        }

        [Fact]
        public void Should_Have_Error_When_SelectedInsuranceProviderIds_Is_Empty()
        {
            var model = new BuildingsInsuranceViewModel { SelectedInsuranceProviderIds = [] };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.SelectedInsuranceProviderIds)
                  .WithErrorMessage("Select who is/are your current buildings insurance provider/s");
        }

        [Fact]
        public void Should_Have_Error_When_IfOtherInsuranceProviderName_Is_Empty_And_Other_Selected()
        {
            var model = new BuildingsInsuranceViewModel
            {
                SelectedInsuranceProviderIds = [11],
                IfOtherInsuranceProviderName = ""
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.IfOtherInsuranceProviderName)
                  .WithErrorMessage("If Other, please tell us who is/are your current building insurance provider/s?");
        }
    }
}