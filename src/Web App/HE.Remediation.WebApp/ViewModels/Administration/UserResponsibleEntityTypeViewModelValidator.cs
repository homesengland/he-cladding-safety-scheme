using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class UserResponsibleEntityTypeViewModelValidator : AbstractValidator<UserResponsibleEntityTypeViewModel>
{
    public UserResponsibleEntityTypeViewModelValidator()
    {
        RuleFor(e => e.ResponsibleEntityType)
            .NotNull()
            .WithMessage("Please select how you would like to register");
    }
}