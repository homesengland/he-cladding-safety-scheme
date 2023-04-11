namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport
{
    public class GetFireRiskAppraisalReportResponse
    {
        public Guid FileId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
    }
}
