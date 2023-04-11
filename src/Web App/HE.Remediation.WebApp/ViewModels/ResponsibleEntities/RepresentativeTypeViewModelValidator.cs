using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentativeTypeViewModelValidator : AbstractValidator<RepresentativeTypeViewModel>
{
    public RepresentativeTypeViewModelValidator()
    {
        RuleFor(x => x.RepresentativeType)
            .NotNull()
            .WithMessage("Select an option");
    }
}