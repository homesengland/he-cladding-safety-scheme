using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class LeaseholderOrPrivateOwnerViewModelValidator : AbstractValidator<LeaseholderOrPrivateOwnerViewModel>
{
    public LeaseholderOrPrivateOwnerViewModelValidator()
    {
        RuleFor(x => x.HasOwners)
            .NotNull()
            .WithMessage("Select an option");

        When(x => x.HasOwners == true, () =>
        {
            RuleFor(x => x.SharedOwnerCount)
                .NotNull()
                .WithMessage("How many shared owners is required")
                .InclusiveBetween(0, 999)
                .WithMessage("How many Shared owners must be between 0 and 999")
                .GreaterThan(0);
        });
    }
}