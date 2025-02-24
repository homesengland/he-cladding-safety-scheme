namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers;

public class GetCheckYourAnswersResponse
{
    public List<LeaseHolderEvidenceFile> LeaseHolderEvidenceFiles { get; set; }

    public bool ReadOnly { get; set; }
}

public class LeaseHolderEvidenceFile
{
    public string Name { get; set; }
}