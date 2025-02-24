using FluentValidation;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationAddressViewModelValidator: AbstractValidator<BuildingDeveloperInformationAddressViewModel>
{
    public BuildingDeveloperInformationAddressViewModelValidator()
    {        
        RuleFor(x => x.OrganisationName)
            .NotEmpty()
            .WithMessage("Organisation name is required")
            .MaximumLength(150)
            .WithMessage("Organisation name cannot exceed 150 characters");

        RuleFor(x => x).SetValidator(new AddressViewModelValidator());        
    }   
}
