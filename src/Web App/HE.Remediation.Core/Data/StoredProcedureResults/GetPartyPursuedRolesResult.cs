using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetPartyPursuedRolesResult
{
    public string OtherPartyPursuedRole { get; set; }
    public IList<EPartyPursuedRole> Roles { get; set; } = new List<EPartyPursuedRole>();

    public class Role
    {
        public Guid Id { get; set; }
        public EPartyPursuedRole PartyPursuedRoleId { get; set; }
    }
}