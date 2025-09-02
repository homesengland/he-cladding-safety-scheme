using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class EvidenceOfThirdPartyContributionViewModelValidator : AbstractValidator<EvidenceOfThirdPartyContributionViewModel>
    {
        public EvidenceOfThirdPartyContributionViewModelValidator()
        {
            RuleFor(x => x.GetEvidenceDetailsResponse)
                .NotNull()
                .WithMessage("Evidence details response cannot be null.");

            RuleFor(x => x.GetEvidenceDetailsResponse.EvidenceDetailsResults)
                .NotNull()
                .WithMessage("Evidence details results cannot be null.")
                .Must(list => list != null && list.Any())
                .WithMessage("Please add at least one evidence before continuing.")
                .When(x => x.GetEvidenceDetailsResponse != null);

            RuleForEach(x => x.GetEvidenceDetailsResponse.EvidenceDetailsResults)
                .Must(result => result?.ThirdPartyName == null || result.ThirdPartyName.Length <= 150)
                .WithMessage("Third party name must not exceed 150 characters.")
                .When(x => x.GetEvidenceDetailsResponse != null && x.GetEvidenceDetailsResponse.EvidenceDetailsResults != null);

            RuleForEach(x => x.GetEvidenceDetailsResponse.EvidenceDetailsResults)
                .Must(result => result?.AttemptDetails == null || result.AttemptDetails.Length <= 500)
                .WithMessage("Attempt details must not exceed 500 characters.")
                .When(x => x.GetEvidenceDetailsResponse != null && x.GetEvidenceDetailsResponse.EvidenceDetailsResults != null);
        }
    }
}
