using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualViewModelValidator : AbstractValidator<RepresentationCompanyOrIndividualViewModel>
{
    public RepresentationCompanyOrIndividualViewModelValidator()
    {
        RuleFor(x => x.ReponsibleEntityType)
            .NotNull()
            .WithMessage("Select an option");
    }
}