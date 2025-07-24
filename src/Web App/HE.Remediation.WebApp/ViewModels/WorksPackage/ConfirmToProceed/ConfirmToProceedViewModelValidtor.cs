using FluentValidation;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.ConfirmToProceed
{
    public class ConfirmToProceedViewModelValidtor : AbstractValidator<ConfirmToProceedViewModel>
    {
        public ConfirmToProceedViewModelValidtor()
        {
            RuleFor(x => x.IsConfirmedToProceed)
                .NotNull()
                .WithMessage("Please select an option to proceed");
        }
    }
}
