using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class BuildingSafetyRegulatorRegistrationCodeViewModelValidator : AbstractValidator<BuildingSafetyRegulatorRegistrationCodeViewModel>
    {
        public BuildingSafetyRegulatorRegistrationCodeViewModelValidator()
        {
            RuleFor(x => x.BuildingSafetyRegulatorRegistrationCode)
                .NotEmpty()
                .WithMessage("Enter the Building Safety Regulator registration code")
                .Matches("^[a-zA-Z0-9]{12}$")
                .WithMessage("Building Safety Regulator registration code must be 12 digits");

        }
    }
}
