using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ThirdPartyContributionsChangedViewModelValidator : AbstractValidator<ThirdPartyContributionsChangedViewModel>
{
    public ThirdPartyContributionsChangedViewModelValidator()
    {
        RuleFor(x => x.ThirdPartyContributionsChanged)
            .NotNull()
            .WithMessage("Select yes if you have received any third party contributions");        
    } 
}
