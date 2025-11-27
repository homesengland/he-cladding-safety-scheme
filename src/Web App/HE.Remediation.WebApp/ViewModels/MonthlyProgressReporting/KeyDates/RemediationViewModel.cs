using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.TagHelpers;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates
{
    public class RemediationViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }

        public DateTime? PreviousFullCompletionOfWorksDate { get; set; }
        public DateTime? PreviousPracticalCompletionDate { get; set; }

        public DateTime? FullCompletionOfWorksDate
        {
            get { return FullCompletionOfWorksMonthYearInput.ToDateTime(); }
            set { FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public MonthYearInputTagHelper.MonthYearInput FullCompletionOfWorksMonthYearInput { get; set; } = new MonthYearInputTagHelper.MonthYearInput();

        public DateTime? PracticalCompletionDate
        {
            get { return PracticalCompletionMonthYearInput.ToDateTime(); }
            set { PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public MonthYearInputTagHelper.MonthYearInput PracticalCompletionMonthYearInput { get; set; } = new MonthYearInputTagHelper.MonthYearInput();

        public ESubmitAction SubmitAction { get; set; }
    }
}
