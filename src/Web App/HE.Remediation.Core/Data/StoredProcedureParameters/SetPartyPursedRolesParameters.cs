namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetPartyPursedRolesParameters
{
    public Guid ApplicationId { get; set; }
    public IEnumerable<int> PartyPursuedRoles { get; set; }
}