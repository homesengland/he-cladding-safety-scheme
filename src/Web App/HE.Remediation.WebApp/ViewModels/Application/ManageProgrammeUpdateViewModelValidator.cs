using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ManageProgrammeUpdateViewModelValidator : AbstractValidator<ManageProgrammeUpdateViewModel>
    {
        public ManageProgrammeUpdateViewModelValidator()
        {
            var yearCutOff = DateTime.Now.Year - 5;
            const string monthRangeError = "Month must be between 1 and 12.";
            const string yearRangeError = "Year is not recent enough.";
            const string comboError = "You must enter both a month and a year, or neither.";

            // Month 

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Must(m => m == null || m.Month == null || (m.Month >= 1 && m.Month <= 12))
                .WithMessage(monthRangeError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Must(m => m == null || m.Month == null || (m.Month >= 1 && m.Month <= 12))
                .WithMessage(monthRangeError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must(m => m == null || m.Month == null || (m.Month >= 1 && m.Month <= 12))
                .WithMessage(monthRangeError);

            // Year

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Must(m => m == null || m.Year == null || (m.Year >= yearCutOff))
                .WithMessage(yearRangeError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Must(m => m == null || m.Year == null || (m.Year >= yearCutOff))
                .WithMessage(yearRangeError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must(m => m == null || m.Year == null || (m.Year >= yearCutOff))
                .WithMessage(yearRangeError);

            // Combo

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Must(m => m == null || (m.Month == null && m.Year == null) || (m.Month != null && m.Year != null))
                .WithMessage(comboError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Must(m => m == null || (m.Month == null && m.Year == null) || (m.Month != null && m.Year != null))
                .WithMessage(comboError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must(m => m == null || (m.Month == null && m.Year == null) || (m.Month != null && m.Year != null))
                .WithMessage(comboError);
        }
    }
}