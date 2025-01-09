using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class GrantFundingSignatoriesViewModelValidator : AbstractValidator<GrantFundingSignatoriesViewModel>
{
    public GrantFundingSignatoriesViewModelValidator()
    {
        RuleFor(x => x.GrantFundingSignatories)
            .Must(x => x?.Any() == true)
            .WithMessage("Please add at least one signatory");
    }
}