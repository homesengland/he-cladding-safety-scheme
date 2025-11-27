using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders
{
    public class LastCommunicationDateViewModelValidator : AbstractValidator<LastCommunicationDateViewModel>
    {
        public LastCommunicationDateViewModelValidator()
        {
            const string notNumberError = "must be a number";
            const string comboError = "You must enter both a month and a year";
            const string futureError = "Date must be in the past";

            // LastCommunicationDateMonthYearInput 

            RuleFor(x => x.LastCommunicationDateMonthYearInput.Month)
                .Cascade(CascadeMode.Stop)
                .Must(IsNullOrNumber).WithMessage("Month " + notNumberError)
                .Must(m => IsRange(m, 1, 12)).WithMessage("Enter a valid month");

            RuleFor(x => x.LastCommunicationDateMonthYearInput.Year)
                .Cascade(CascadeMode.Stop)
                .Must(IsNullOrNumber).WithMessage("Year " + notNumberError)
                .Must(m => IsRange(m, 2000, 3000)).WithMessage("Enter a valid year");

            RuleFor(x => x.LastCommunicationDateMonthYearInput)
                .Cascade(CascadeMode.Stop)
                .Must(m => IsValidCombination(m?.Month, m?.Year)).WithMessage(comboError)
                .Must(m => IsNotInTheFuture(m?.Month, m?.Year) != false).WithMessage(futureError);
        }

        private static bool IsRange(string m, int start, int end)
        {
            if (m == null) return true;
            return int.TryParse(m.ToString(), out var val) && val >= start && val <= end;
        }

        private static bool IsNullOrNumber(string m)
        {
            return m == null || int.TryParse(m.ToString(), out _);
        }

        private static bool IsValidCombination(string m, string y)
        {
            var hasMonth = !string.IsNullOrWhiteSpace(m);
            var hasYear = !string.IsNullOrWhiteSpace(y);
            return hasMonth && hasYear;
        }

        private static bool? IsNotInTheFuture(string m, string y)
        {
            if (!int.TryParse(y, out var year) || !int.TryParse(m, out var month))
            {
                return null;
            }
            if (month < 1 || month > 12)
            {
                return null;
            }
            var fullDate = new DateOnly(year, month, 1);
            var todayDate = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var isValid = fullDate <= todayDate;

            return isValid;
        }
    }
}
