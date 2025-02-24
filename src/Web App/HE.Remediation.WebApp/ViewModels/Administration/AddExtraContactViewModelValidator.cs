using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class AddExtraContactViewModelValidator: AbstractValidator<AddExtraContactViewModel>
{
    public AddExtraContactViewModelValidator()
    {
        RuleFor(x => x.AddContact)
            .NotNull()
            .WithMessage("Select an option");
    }
}
