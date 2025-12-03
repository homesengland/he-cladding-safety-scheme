using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.TagHelpers;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates
{
    public class BuildingControlViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public DateTime? PreviousBuildingControlExpectedApplicationDate { get; set; }
        public DateTime? PreviousBuildingControlActualApplicationDate { get; set; }
        public DateTime? PreviousBuildingControlValidationDate { get; set; }
        public DateTime? PreviousBuildingControlDecisionDate { get; set; }

        public DateTime? BuildingControlExpectedApplicationDate
        {
            get { return BuildingControlExpectedApplicationDateMonthYearInput.ToDateTime(); }
            set { BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public DateTime? BuildingControlActualApplicationDate
        {
            get { return BuildingControlActualApplicationDateMonthYearInput.ToDateTime(); }
            set { BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public DateTime? BuildingControlValidationDate
        {
            get { return BuildingControlValidationDateMonthYearInput.ToDateTime(); }
            set { BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public DateTime? BuildingControlDecisionDate
        {
            get { return BuildingControlDecisionDateMonthYearInput.ToDateTime(); }
            set { BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public MonthYearInputTagHelper.MonthYearInput BuildingControlExpectedApplicationDateMonthYearInput { get; set; } = new MonthYearInputTagHelper.MonthYearInput();
        public MonthYearInputTagHelper.MonthYearInput BuildingControlActualApplicationDateMonthYearInput { get; set; } = new MonthYearInputTagHelper.MonthYearInput();
        public MonthYearInputTagHelper.MonthYearInput BuildingControlValidationDateMonthYearInput { get; set; } = new MonthYearInputTagHelper.MonthYearInput();
        public MonthYearInputTagHelper.MonthYearInput BuildingControlDecisionDateMonthYearInput { get; set; } = new MonthYearInputTagHelper.MonthYearInput();

        public string Gateway2Reference { get; set; }
        public EBuildingControlDecisionType? BuildingControlDecisionType { get; set; }

        public ESubmitAction SubmitAction { get; set; }
    }
}
