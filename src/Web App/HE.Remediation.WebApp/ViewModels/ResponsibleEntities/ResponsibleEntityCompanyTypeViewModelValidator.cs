using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityCompanyTypeViewModelValidator : AbstractValidator<ResponsibleEntityCompanyTypeViewModel>
{
    public ResponsibleEntityCompanyTypeViewModelValidator()
    {
        RuleFor(x => x.OrganisationType)
            .NotNull()
            .WithMessage("Select an option")
            .IsInEnum()
            .WithMessage("Please select a valid company type");

        When(x => x.OrganisationType == EApplicationResponsibleEntityOrganisationType.Other, () =>
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
        });
    }
}