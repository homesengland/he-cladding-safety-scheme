namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetRemoveTeamMember;

public class GetRemoveTeamMemberResponse
{    
    public Guid TeamMemberId { get; set; }

    public string TeamMemberName { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}
