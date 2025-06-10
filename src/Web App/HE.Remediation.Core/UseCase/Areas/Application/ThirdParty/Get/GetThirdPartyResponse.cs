using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;

public class GetThirdPartyResponse
{
    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public List<ETeamRole> MissingRoles { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
}
