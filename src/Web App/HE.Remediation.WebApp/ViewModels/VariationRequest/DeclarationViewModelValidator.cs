using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class DeclarationViewModelValidator : AbstractValidator<DeclarationViewModel>
{
    public DeclarationViewModelValidator()
    {
        RuleFor(x => x.ConfirmedAwareOfApproval)
            .Must(x => x == true)
            .WithMessage("Confirm that you are aware that Homes England approves each variation on its individual merits and that approval does not set a precedent for future variation requests.");

        RuleFor(x => x.ConfirmedCostsReasonable)
            .Must(x => x == true)
            .WithMessage("Confirm that all cost details are reasonable and correct and have been profiled accurately in accordance with the works contract.");

        RuleFor(x => x.ConfirmedCoversRecommendations)
            .Must(x => x == true)
            .WithMessage("Confirm that the works cover the recommendations as specified in the FRAEW summary to address the fire safety risks of the building.");
    }
}
