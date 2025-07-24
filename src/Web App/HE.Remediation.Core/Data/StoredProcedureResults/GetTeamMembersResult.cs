namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetTeamMembersResult
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }

    public int? RoleId { get; set; }

    public string Name { get; set; }

    public string CompanyName { get; set; }

    public string OtherRole { get; set; }

    public bool? HasChasCertification { get; set; }
    public string EmailAddress { get; set; }
    //public int InviteStatus { get; set; }
    //public bool? IsRevoked { get; set; }
}
