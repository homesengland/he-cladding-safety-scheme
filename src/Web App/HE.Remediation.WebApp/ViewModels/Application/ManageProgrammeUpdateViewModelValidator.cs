using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ManageProgrammeUpdateViewModelValidator : AbstractValidator<ManageProgrammeUpdateViewModel>
    {
        public ManageProgrammeUpdateViewModelValidator()
        {
            var yearCutOff = DateTime.Now.Year - 5;
            const string notNumberError = "must be a number";
            const string monthRangeError = "Month must be between 1 and 12";
            const string yearRangeError = "Year is not recent enough";
            const string comboError = "You must enter both a month and a year, or neither";

            // Month 

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Month)).WithMessage("Month " + notNumberError)
                .Must(m => IsRange(m.Month, 1, 12)).WithMessage(monthRangeError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Month)).WithMessage("Month " + notNumberError)
                .Must(m => IsRange(m.Month, 1, 12)).WithMessage(monthRangeError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Month)).WithMessage("Month " + notNumberError)
                .Must(m => IsRange(m.Month, 1, 12)).WithMessage(monthRangeError);

            // Year

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Year)).WithMessage("Year " + notNumberError)
                .Must(m => IsGreater(m.Year, yearCutOff)).WithMessage(yearRangeError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Year)).WithMessage("Year " + notNumberError)
                .Must(m => IsGreater(m.Year, yearCutOff)).WithMessage(yearRangeError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Year)).WithMessage("Year " + notNumberError)
                .Must(m => IsGreater(m.Year, yearCutOff)).WithMessage(yearRangeError);

            // Combo

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Must(m => IsNullOrComplete(m.Month, m.Year)).WithMessage(comboError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Must(m => IsNullOrComplete(m.Month, m.Year)).WithMessage(comboError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must(m => IsNullOrComplete(m.Month, m.Year)).WithMessage(comboError);
        }

        private static bool IsNullOrNumber(string m)
        {
            return m == null || int.TryParse(m.ToString(), out _);
        }

        private static bool IsNullOrComplete(string m, string y)
        {
            return m == null || (m == null && y == null) || (m != null && y != null);
        }

        private static bool IsRange(string m, int start, int end)
        {
            if (m == null) return true;
            return int.TryParse(m.ToString(), out var val) && val >= start && val <= end;
        }

        private static bool IsGreater(string m, int start)
        {
            if (m == null) return true;
            return int.TryParse(m.ToString(), out var val) && val >= start;
        }
    }
}