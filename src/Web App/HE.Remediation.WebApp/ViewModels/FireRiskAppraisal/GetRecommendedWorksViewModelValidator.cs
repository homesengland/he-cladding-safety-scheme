using FluentValidation;

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

        RuleFor(x => x.RecommendedTotalAreaCladding)
                .NotEmpty()
                .WithMessage("Please enter a value");
        
        RuleFor(x => x.CaveatsLimitations)
                .NotEmpty()
                .WithMessage("Please enter a value")
                .MaximumLength(1000)
                .WithMessage("Text needs to be less than 1000 characters");

        RuleFor(x => x.RemediationSummary)
                .NotEmpty()
                .WithMessage("Please enter a value")
                .MaximumLength(1000)
                .WithMessage("Text needs to be less than 1000 characters"); ;

        RuleFor(x => x.JustifyRecommendation)
                .NotEmpty()
                .WithMessage("Please enter a value")
                .MaximumLength(1000)
                .WithMessage("Text needs to be less than 1000 characters"); ;
    }
}
