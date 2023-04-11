using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class BuildingPartOfDevelopmentViewModelValidator : AbstractValidator<BuildingPartOfDevelopmentViewModel>
    {

        public BuildingPartOfDevelopmentViewModelValidator()
        {
            RuleFor(x => x.PartOfDevelopment)
                .NotNull()
                .WithMessage("Select an option");
        }
    }
}
