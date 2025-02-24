using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class SubContractorRatingsViewModelValidator : AbstractValidator<SubContractorRatingsViewModel>
{
    public SubContractorRatingsViewModelValidator()
    {
        RuleFor(x => x.Ratings.QualityOfWork)
            .NotNull()
            .WithMessage("Please select a rating for Quality of work");
        
        RuleFor(x => x.Ratings.ValueForMoney)
            .NotNull()
            .WithMessage("Please select a rating for Value for money");
        
        RuleFor(x => x.Ratings.Reliability)
            .NotNull()
            .WithMessage("Please select a rating for Reliability");
        
        RuleFor(x => x.Ratings.ConsiderationOfResidents)
            .NotNull()
            .WithMessage("Please select a rating for Consideration of residents");
        
        RuleFor(x => x.Ratings.OverallSatisfaction)
            .NotNull()
            .WithMessage("Please select a rating for Overall satisfaction");
    }
}