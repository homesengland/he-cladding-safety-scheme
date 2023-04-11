using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class BasedInUkViewModelValidator : AbstractValidator<BasedInUkViewModel>
{
    public BasedInUkViewModelValidator()
    {
        RuleFor(x => x.BasedInUk)
            .NotNull()
            .WithMessage("Select an option");
    }
}