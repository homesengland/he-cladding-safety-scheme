using HE.Remediation.Core.Enums;

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
}
