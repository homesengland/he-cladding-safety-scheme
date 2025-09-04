using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class ContractorContingencyViewModelValidator : AbstractValidator<ContractorContingencyViewModel>
{
    public ContractorContingencyViewModelValidator()
    {
        RuleFor(x => x.UsedVariationContractorContingency)
            .NotNull()
            .WithMessage("Select an option");
        RuleFor(x => x.ContractorContingencyAdditionalNotes)
                .NotEmpty()
                .WithMessage("Please explain your answer")
                .MaximumLength(2000)
                .WithMessage("Additional notes up to 2000 characters.");
    }
}
