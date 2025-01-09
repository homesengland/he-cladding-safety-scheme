using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;

public class GrantCertifyingOfficerCandidateResult
{
    public Guid ProjectTeamMemberId { get; set; }

    public string Name { get; set; }

    public string RoleName { get; set; }
}
