using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityUkRegisteredViewModelValidator : AbstractValidator<ResponsibleEntityUkRegisteredViewModel>
{
    public ResponsibleEntityUkRegisteredViewModelValidator()
    {
        RuleFor(x => x.UkRegistered)
            .NotNull()
            .WithMessage("Select an option");
    }
}