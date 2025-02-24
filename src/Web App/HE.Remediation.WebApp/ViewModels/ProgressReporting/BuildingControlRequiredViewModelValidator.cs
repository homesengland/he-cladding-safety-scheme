using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class BuildingControlRequiredViewModelValidator: AbstractValidator<BuildingControlRequiredViewModel>
    {
        public BuildingControlRequiredViewModelValidator()
        {
            RuleFor(x => x.BuildingControlRequired)
                .NotNull()
                .WithMessage("Please select Yes or No");
        }
    }
}
