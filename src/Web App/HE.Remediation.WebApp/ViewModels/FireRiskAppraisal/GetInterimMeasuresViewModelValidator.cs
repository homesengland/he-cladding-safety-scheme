using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class GetInterimMeasuresViewModelValidator : AbstractValidator<GetInterimMeasuresViewModel>
    {
        public GetInterimMeasuresViewModelValidator()
        {
            RuleFor(x => x.BuildingInterimMeasures)
                .NotNull()
                .WithMessage("Select an option - Interim measures");

            RuleFor(x => x.BuildingInterimMeasuresText)
                .NotEmpty()
                .When(x => x.BuildingInterimMeasuresTypes.Contains(EInterimMeasuresType.Other) && x.BuildingInterimMeasures == EYesNoNonBoolean.Yes)
                .WithMessage("Description required - Other interim measures")
                .MaximumLength(150)
                .WithMessage("The maximum number of characters allowed is 150");

            RuleFor(x => x.EvacuationStrategyType)
                .NotNull()
                .WithMessage("Select an option - Evacuation strategy");

            RuleFor(x => x.EvacuationStrategyText)
                .NotEmpty()
                .When(x => x.EvacuationStrategyType == EEvacuationStrategy.Other)
                .WithMessage("Description required - Other current evacuation strategy")
                .MaximumLength(150)
                .WithMessage("The maximum number of characters allowed is 150");

            RuleFor(x => x.NumberOfStairwellsPrompt)
                .NotNull()
                .WithMessage("Select an option - Number of Stairwells");

            RuleFor(x => x.NumberOfStairwells)
                .NotEmpty()
                .WithMessage("Please enter number of stairwells")
                .GreaterThan(0)
                .WithMessage("Please enter number of stairwells")
                .LessThanOrEqualTo(int.MaxValue)
                .When(x => x.NumberOfStairwellsPrompt == ENoYes.Yes);

            RuleFor(x => x.ExternalWallAndBalconiesPolicy)
                .NotNull()
                .WithMessage("Select an option - External wall and balconies");

            RuleFor(x => x.FireAndResueAccessRestrictions)
                .NotNull()
                .WithMessage("Select an option - Access restrictions");

            RuleFor(x => x.FireAndResueAccessRestrictionsText)
                .NotEmpty()
                .When(x => x.FireAndResueAccessRestrictions == EYesNoNonBoolean.Yes)
                .WithMessage("Description required - Access restrictions")
                .MaximumLength(150)
                .WithMessage("The maximum number of characters allowed is 150");
        }
    }
}