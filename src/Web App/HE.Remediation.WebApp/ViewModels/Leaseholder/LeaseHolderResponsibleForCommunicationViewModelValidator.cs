using FluentValidation;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderResponsibleForCommunicationViewModelValidator : AbstractValidator<LeaseHolderResponsibleForCommunicationViewModel>
    {

        public LeaseHolderResponsibleForCommunicationViewModelValidator()
        {
            RuleFor(x => x.ResponsibleForCommunication)
                .NotNull()
                .WithMessage("Select an option");
        }
    }
}
