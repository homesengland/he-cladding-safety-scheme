
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class FinalCheckYourAnswersViewModel : ClosingReportBaseViewModel
{
    public List<ClosingReportUploadFile> ExitFraewFiles { get; set; }
    public List<ClosingReportUploadFile> PracticalCompletionCertificateFiles { get; set; }
    public List<ClosingReportUploadFile> BuildingRegulationsFiles { get; set; }
    public List<ClosingReportUploadFile> LeaseholderResidentCommunication { get; set; }
    public List<ClosingReportUploadFile> FinalCostFiles { get; set; }
    public decimal? FinalPaymentRequest { get; set; }
}

public class ClosingReportUploadFile
{
    public string Name { get; set; }
    public EClosingReportFileType UploadType { get; set; }
}