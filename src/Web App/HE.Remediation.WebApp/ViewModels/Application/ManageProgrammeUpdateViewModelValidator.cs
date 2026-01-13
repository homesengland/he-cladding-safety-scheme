using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ManageProgrammeUpdateViewModelValidator : AbstractValidator<ManageProgrammeUpdateViewModel>
    {
        public ManageProgrammeUpdateViewModelValidator()
        {
            var minDate = new DateTime(2017, 1, 1);
            const string notNumberError = "must be a number";
            const string monthRangeError = "Month must be between 1 and 12";
            const string comboError = "You must enter both a month and a year, or neither";
            const string minDateError = "Dates cannot be before 01/2017";
            const string startOnSiteBeforeInvestigationError = "Estimated start on site date cannot be before the estimated works investigation completion date";
            const string practicalCompletionBeforeInvestigationError = "Estimated practical completion date cannot be before the estimated works investigation completion date";
            const string practicalCompletionBeforeStartOnSiteError = "Estimated practical completion date cannot be before estimated start on site date";

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
                .Must(m => IsNullOrNumber(m.Year)).WithMessage("Year " + notNumberError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Year)).WithMessage("Year " + notNumberError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsNullOrNumber(m.Year)).WithMessage("Year " + notNumberError);

            // Combo

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Must(m => IsNullOrComplete(m.Month, m.Year)).WithMessage(comboError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Must(m => IsNullOrComplete(m.Month, m.Year)).WithMessage(comboError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must(m => IsNullOrComplete(m.Month, m.Year)).WithMessage(comboError);

            // Minimum date validation (01/2017)

            RuleFor(x => x.EstimatedInvestigationCompletion)
                .Must(m => IsAfterOrEqualMinDate(m, minDate)).WithMessage(minDateError);

            RuleFor(x => x.EstimatedStartOnSite)
                .Must(m => IsAfterOrEqualMinDate(m, minDate)).WithMessage(minDateError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must(m => IsAfterOrEqualMinDate(m, minDate)).WithMessage(minDateError);

            // Date comparison validations

            RuleFor(x => x.EstimatedStartOnSite)
                .Must((model, startOnSite) => IsDateAfterOrEqual(startOnSite, model.EstimatedInvestigationCompletion))
                .WithMessage(startOnSiteBeforeInvestigationError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must((model, practicalCompletion) => IsDateAfterOrEqual(practicalCompletion, model.EstimatedInvestigationCompletion))
                .WithMessage(practicalCompletionBeforeInvestigationError);

            RuleFor(x => x.EstimatedPracticalCompletion)
                .Must((model, practicalCompletion) => IsDateAfterOrEqual(practicalCompletion, model.EstimatedStartOnSite))
                .WithMessage(practicalCompletionBeforeStartOnSiteError);
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

        private static bool IsAfterOrEqualMinDate(HE.Remediation.WebApp.TagHelpers.MonthYearInputTagHelper.MonthYearInput input, DateTime minDate)
        {
            var date = input?.ToDateTime();
            if (date == null) return true;
            return date.Value >= minDate;
        }

        private static bool IsDateAfterOrEqual(HE.Remediation.WebApp.TagHelpers.MonthYearInputTagHelper.MonthYearInput laterDate, HE.Remediation.WebApp.TagHelpers.MonthYearInputTagHelper.MonthYearInput earlierDate)
        {
            var later = laterDate?.ToDateTime();
            var earlier = earlierDate?.ToDateTime();
            
            // If either date is null, skip comparison validation
            if (later == null || earlier == null) return true;
            
            return later.Value >= earlier.Value;
        }
    }
}