using FluentValidation;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationViewModelValidator : AbstractValidator<BuildingDeveloperInformationViewModel>
{
    public BuildingDeveloperInformationViewModelValidator()
    {
        RuleFor(x => x.DoYouKnowOriginalDeveloper)
            .NotNull()
            .WithMessage("Select an option");

        When(x => x.DoYouKnowOriginalDeveloper == true, () =>
        {
            RuleFor(x => x.OrganisationName)
                .NotEmpty()
                .WithMessage("Organisation name is required")
                .MaximumLength(150)
                .WithMessage("Organisation name cannot exceed 150 characters");

            RuleFor(x => x).SetValidator(new AddressViewModelValidator());
        });
    }
}