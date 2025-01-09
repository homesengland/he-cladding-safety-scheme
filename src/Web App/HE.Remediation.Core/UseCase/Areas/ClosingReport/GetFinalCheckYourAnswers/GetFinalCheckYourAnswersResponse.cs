using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetFinalCheckYourAnswers;

public class GetFinalCheckYourAnswersResponse
{
    public List<FileResult> ExitFraewFiles { get; set; }

    public List<FileResult> PracticalCompletionCertificateFiles { get; set; }

    public List<FileResult> BuildingRegulationsFiles { get; set; }

    public List<FileResult> LeaseholderResidentCommunication { get; set; }

    public List<FileResult> FinalCostFiles { get; set; }

    public decimal? FinalPaymentRequest { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}
