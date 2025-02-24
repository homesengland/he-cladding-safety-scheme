
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Get;

public class GetDeclarationResponse
{
    public bool? AllCostsReasonable { get; set; }
    
    public bool? AllContractualRequirementsMet { get; set; }

    public bool? AllCostsSetOutInFull { get; set; }

    public bool? AcceptGrantAwardBasedOnCosts { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    
    public bool IsSubmitted { get; set; }
}
