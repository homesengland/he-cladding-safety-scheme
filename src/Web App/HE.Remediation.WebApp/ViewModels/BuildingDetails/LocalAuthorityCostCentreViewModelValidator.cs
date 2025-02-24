using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class LocalAuthorityCostCentreViewModelValidator : AbstractValidator<LocalAuthorityCostCentreViewModel>
    {
        public LocalAuthorityCostCentreViewModelValidator()
        {
            RuleFor(x => x.LocalAuthorityCostCentreId)
                            .NotNull()
                            .WithMessage("Please select a local authority")
                            .Must(BeValidLocalAuthorityCostCentreDropDownSelection)                
                            .WithMessage("Please select a local authority");
        }
        private bool BeValidLocalAuthorityCostCentreDropDownSelection(string inputStr)
        {
            return (inputStr != null) && (inputStr != "0");
        }
    }
}
