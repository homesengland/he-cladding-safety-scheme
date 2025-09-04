using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class OtherPartiesViewModelValidator : AbstractValidator<OtherPartiesViewModel>
{
    public OtherPartiesViewModelValidator()
    {
        RuleFor(x => x.OtherPartyPursuedRole)
            .NotEmpty()
            .WithMessage("Enter other parties")
            .MaximumLength(500)
            .WithMessage("Other parties cannot be more than 500 characters");
    }
}