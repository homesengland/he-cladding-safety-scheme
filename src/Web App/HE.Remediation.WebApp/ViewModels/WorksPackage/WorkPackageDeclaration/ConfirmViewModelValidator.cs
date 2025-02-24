using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageDeclaration;

public class ConfirmViewModelValidator : AbstractValidator<ConfirmViewModel>
{
    public ConfirmViewModelValidator()
    {
        RuleFor(x => x.AllContractualRequirementsMet)
            .Must(x => x == true)
            .WithMessage("Confirm that the works cover the recommendations as specified in your FRAEW summary to address the fire safety risks of the building.");
        
        RuleFor(x => x.AllCostsReasonable)
            .Must(x => x == true)
            .WithMessage("Confirm that to the best of your knowledge, all costs details are reasonable and correct.");

        RuleFor(x => x.AllCostsSetOutInFull)
            .Must(x => x == true)
            .WithMessage("Confirm that all costs have been set out in full.");

        RuleFor(x => x.AcceptGrantAwardBasedOnCosts)
            .Must(x => x == true)
            .WithMessage("Confirm I accept that the grant award will be calculated based on the costs I have provided.");
    }
}
