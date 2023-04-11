using FluentValidation;

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
    }
}