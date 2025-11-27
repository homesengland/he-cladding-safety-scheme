using HE.Remediation.WebApp.TagHelpers;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders
{
    public class LastCommunicationDateViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }

        public DateTime? LastCommunicationDate
        {
            get { return LastCommunicationDateMonthYearInput.ToDateTime(); }
            set { LastCommunicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public MonthYearInputTagHelper.MonthYearInput LastCommunicationDateMonthYearInput { get; set; }

        public DateTime? PreviousLastCommunicationDate { get; set; }
    }
}