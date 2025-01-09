using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityResponsibleForGrantFundingViewModelValidator : AbstractValidator<ResponsibleEntityResponsibleForGrantFundingViewModel>
{
    public ResponsibleEntityResponsibleForGrantFundingViewModelValidator()
    {
        RuleFor(x => x.ResponsibleForGrantFunding)
            .NotNull()
            .WithMessage("Select an option");
    }
}