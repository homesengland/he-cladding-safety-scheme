
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Get;

public class GetConfirmSignatoriesResponse
{
    public bool? AreSignatoriesCorrect { get; set; }
    
    public IEnumerable<string> Signatories { get; set; }
 
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    
    public bool IsSubmitted { get; set; }
}
