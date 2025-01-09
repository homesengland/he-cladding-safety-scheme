using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Get;

public class GetSelectResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    
    public bool IsSubmitted { get; set; }
    
    public Guid? SelectedProjectTeamMemberId { get; set; }

    public IReadOnlyCollection<GrantCertifyingOfficerCandidateResult> Candidates { get; set; }
}
