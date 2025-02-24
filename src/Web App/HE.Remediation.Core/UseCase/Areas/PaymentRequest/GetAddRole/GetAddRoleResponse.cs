using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetAddRole;

public class GetAddRoleResponse
{
    public List<ETeamRole> AvailableTeamRoles { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}
