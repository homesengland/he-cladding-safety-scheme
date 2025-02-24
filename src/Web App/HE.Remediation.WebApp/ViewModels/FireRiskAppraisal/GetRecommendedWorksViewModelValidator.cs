using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetRecommendedWorksViewModelValidator: AbstractValidator<GetRecommendedWorksViewModel>
{
    public GetRecommendedWorksViewModelValidator()
    {
        RuleFor(x => x.LifeSafetyRiskAssessment)
                .NotEmpty()
                .WithMessage("Please select a value");
        
        RuleFor(x => x.RecommendCladding)
                .NotEmpty()
                .WithMessage("Please select a value");

        RuleFor(x => x.RecommendBuildingIntetim)
                .NotEmpty()
                .WithMessage("Please select a value");
        
        RuleFor(x => x.CaveatsLimitations)
                .NotEmpty()
                .WithMessage("Please enter a value")
                .MaximumLength(2000)
                .WithMessage("Text needs to be less than 2000 characters");

        RuleFor(x => x.RemediationSummary)
                .NotEmpty()
                .WithMessage("Please enter a value")
                .MaximumLength(2000)
                .WithMessage("Text needs to be less than 2000 characters");

        RuleFor(x => x.OtherRiskMitigationOptionsConsidered)
                .NotEmpty()
                .WithMessage("Please enter a value")
                .MaximumLength(2000)
                .WithMessage("Text needs to be less than 2000 characters");

        RuleFor(x => x.OtherInterimMeasuresText)
            .NotEmpty()
            .When(x => x.RecommendedInterimMeasuresTypes.Any(x => x.Equals(EInterimMeasuresType.Other)))
            .WithMessage("Please enter a value");

        RuleFor(x => x.SafetyRiskOtherText)
            .NotEmpty()
            .When(x => x.RiskSafetyMitigationTypes.Any(x => x.Equals(ERiskSafetyMitigationType.Other)))
            .WithMessage("Please enter a value");

        RuleFor(x => x.RiskSafetyMitigationTypes)
            .NotEmpty()
            .WithMessage("Select an option - Safety Risk Mitigation Types");

        RuleFor(x => x.RecommendedInterimMeasuresTypes)
            .NotEmpty()
            .When(x => x.RecommendBuildingIntetim == Core.Enums.ENoYes.Yes)
            .WithMessage("Select an option - Interim measures");
    }
}
