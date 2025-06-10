using FluentValidation;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderResponsibleForCommunicationTypeViewModelValidator : AbstractValidator<LeaseHolderResponsibleForCommunicationTypeViewModel>
    {

        public LeaseHolderResponsibleForCommunicationTypeViewModelValidator()
        {
            RuleFor(x => x.ResponsibleForCommunicationTypeId)
                .NotNull()
                .WithMessage("Select an option");

            RuleFor(x => x.RepresentationOtherText)
                .MaximumLength(50)
                .WithMessage("Text cannot exceed 50 characters");

            RuleFor(x => x.RepresentationOtherText)
                .NotEmpty()
                .When(x => x.ResponsibleForCommunicationTypeId == EResponsibleForCommunicationType.Other)
                .WithMessage("Enter other text");
        }
    }
}
