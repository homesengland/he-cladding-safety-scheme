using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ThirdPartyViewModel
    {
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public List<GetTeamMembersResult> TeamMembers { get; set; }
        public bool? HasChasCertificationValue { get; set; }
    }
}
