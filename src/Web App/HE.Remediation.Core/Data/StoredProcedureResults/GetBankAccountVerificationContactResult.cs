using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetBankAccountVerificationContactResult
{
    public EBankDetailsRelationship ResponsibleEntityRelationship { get; set; }
    public string VerificationContactName { get; set; }
    public string VerificationContactNumber { get; set; }
}