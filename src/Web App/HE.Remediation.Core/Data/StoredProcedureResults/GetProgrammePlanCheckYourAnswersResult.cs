namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetProgrammePlanCheckYourAnswersResult
{
    public bool? HasProgrammePlan { get; set; }
    public List<string> FileNames { get; set; } = new List<string>();
}