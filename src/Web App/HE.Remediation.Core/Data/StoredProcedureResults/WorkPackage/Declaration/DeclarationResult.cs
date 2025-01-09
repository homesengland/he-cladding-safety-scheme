namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.Declaration;

public class DeclarationResult
{
    public bool? AllCostsReasonable { get; set; }
    
    public bool? AllContractualRequirementsMet { get; set; }

    public bool? AllCostsSetOutInFull { get; set; }

    public bool? AcceptGrantAwardBasedOnCosts { get; set; }
}