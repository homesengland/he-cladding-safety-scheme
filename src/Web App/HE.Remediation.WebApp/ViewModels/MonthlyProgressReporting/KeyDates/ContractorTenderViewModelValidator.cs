using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class ContractorTenderViewModelValidator : AbstractValidator<ContractorTenderViewModel>
{
    public ContractorTenderViewModelValidator()
    {
        RuleFor(x => x.ContractorTenderType)
            .NotNull().WithMessage("Select the type of contractor tender");
    }
}
