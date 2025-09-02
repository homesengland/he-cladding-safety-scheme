using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling
{
    public class PreferredContractorLinksViewModelValidator : AbstractValidator<PreferredContractorLinksViewModel>
    {
        public PreferredContractorLinksViewModelValidator()
        {
            RuleFor(x => x.PreferredContractorLinks)
                .NotNull()
                .WithMessage("Select an option.");
            RuleFor(x => x.PreferredContractorLinkAdditionalNotes)
                .NotEmpty()
                .WithMessage("Enter additional notes.")
                .MaximumLength(2000)
                .WithMessage("Additional notes up to 2000 characters.");
        }
    }
}
