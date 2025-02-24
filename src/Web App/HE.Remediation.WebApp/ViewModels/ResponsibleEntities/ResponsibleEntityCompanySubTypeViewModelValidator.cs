using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityCompanySubTypeViewModelValidator : AbstractValidator<ResponsibleEntityCompanySubTypeViewModel>
{
    public ResponsibleEntityCompanySubTypeViewModelValidator()
    {
        RuleFor(x => x.OrganisationSubType)
            .NotNull()
            .WithMessage("Select an option");

        When(x => x.OrganisationSubType == EApplicationResponsibleEntityOrganisationSubType.OtherCompanyType, () =>
        {
            RuleFor(x => x.OrganisationSubTypeDescription)
                .NotEmpty()
                .WithMessage("Company type description is required")
                .MaximumLength(1000)
                .WithMessage("Company type description cannot exceed 1000 characters");
        });
    }
}
