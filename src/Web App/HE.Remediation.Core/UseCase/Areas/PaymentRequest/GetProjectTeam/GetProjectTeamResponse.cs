using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectTeam;

public class GetProjectTeamResponse
{
    public bool? ThirdPartyContributionsChanged { get; set; }
    public bool? CostsChanged { get; set; }
    public bool EndDateSlipped { get; set; }

    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public List<ETeamRole> MissingRoles { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}
