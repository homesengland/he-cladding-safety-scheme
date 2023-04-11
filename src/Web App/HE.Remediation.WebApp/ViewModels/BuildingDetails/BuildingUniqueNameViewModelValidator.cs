using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class BuildingUniqueNameViewModelValidator : AbstractValidator<BuildingUniqueNameViewModel>
    {
        public BuildingUniqueNameViewModelValidator()
        {
            RuleFor(x => x.UniqueName)
                .NotEmpty()
                .WithMessage("Enter a Unique name for this building")
                .MaximumLength(200)
                .WithMessage("Building name cannot exceed 200 characters");
        }
    }
}
