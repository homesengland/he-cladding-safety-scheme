
namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.WorkPackageSignatories;

public class SignatoriesResult
{
    public bool? AreSignatoriesCorrect { get; set; }
    
    public bool? ContactTaskRaised { get; set; }

    public IEnumerable<string> Signatories { get; set; }
}