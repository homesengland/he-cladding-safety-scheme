using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport
{
    public class GetFireRiskAssessmentReportResponse
    {
        public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
        public FileResult AddedFra { get; set; }
    }

    public class FileResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
    }
}
