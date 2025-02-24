using GovUk.Frontend.AspNetCore;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class LeaseholdersInformedLastViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public DateTime? LeaseholdersInformedLastDate { get; set; }
        public ESubmitAction SubmitAction { get; set; }
        public string ReturnUrl { get; set; }
    }
}
