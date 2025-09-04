using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme
{     
    public class GetManageProgrammeResponse
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public string UniqueBuildingName { get; set; }
        public EApplicationScheme ApplicationScheme { get; set; }
        public DateTime? InvestigationCompletionDate { get; set; }
        public DateTime? StartOnSiteDate { get; set; }
        public DateTime? PracticalCompletionDate { get; set; }
        
    }
}
