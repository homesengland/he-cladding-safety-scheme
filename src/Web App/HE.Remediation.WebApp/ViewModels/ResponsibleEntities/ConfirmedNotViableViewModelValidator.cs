using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ConfirmedNotViableViewModelValidator : AbstractValidator<ConfirmedNotViableViewModel>
{
    public ConfirmedNotViableViewModelValidator()
    {
        RuleFor(x => x.IsConfirmedNotViable)
            .NotNull()
            .WithMessage("Select an option");
    }
}