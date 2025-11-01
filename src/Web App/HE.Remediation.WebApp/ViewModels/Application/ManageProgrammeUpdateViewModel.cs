using AutoMapper.Internal.Mappers;
using HE.Remediation.WebApp.TagHelpers;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ManageProgrammeUpdateViewModel
    {
        public string[] ApplicationHeadlines { get; set; }
        public DateTime? EstimatedInvestigationCompletionDate { 
            get { return EstimatedInvestigationCompletion.ToDateTime(); } 
            set { EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; } 
        }
        public DateTime? EstimatedStartOnSiteDate
        {
            get { return EstimatedStartOnSite.ToDateTime(); }
            set { EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }
        public DateTime? EstimatedPracticalCompletionDate
        {
            get { return EstimatedPracticalCompletion.ToDateTime(); }
            set { EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput() { Month = value?.Month.ToString(), Year = value?.Year.ToString() }; }
        }

        public MonthYearInputTagHelper.MonthYearInput EstimatedInvestigationCompletion { get; set; }
        public MonthYearInputTagHelper.MonthYearInput EstimatedStartOnSite { get; set; }
        public MonthYearInputTagHelper.MonthYearInput EstimatedPracticalCompletion { get; set; }
        public IReadOnlyCollection<string> AppId { get; set; }
    }
}
