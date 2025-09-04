using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CheckYourAnswersViewModelValidator : AbstractValidator<CheckYourAnswersViewModel>
{
    public CheckYourAnswersViewModelValidator()
    {
        RuleFor(x => x.OtherPartyPursuedRole)
            .NotEmpty()
            .WithMessage("Enter other parties")
            .When(x => x.IsSocialSector && x.PartyPursuedRoles.Any(r => r.Id == EPartyPursuedRole.Other), ApplyConditionTo.AllValidators);

        RuleFor(x => x.FundingRouteTypes)
            .NotEmpty()
            .WithMessage("Enter other funding routes")
            .When(x => !x.IsSocialSector && x.OtherSourcesPursuedTypeId == EPursuedSourcesFundingType.PursuingOtherRoutes, ApplyConditionTo.AllValidators);
    }
}