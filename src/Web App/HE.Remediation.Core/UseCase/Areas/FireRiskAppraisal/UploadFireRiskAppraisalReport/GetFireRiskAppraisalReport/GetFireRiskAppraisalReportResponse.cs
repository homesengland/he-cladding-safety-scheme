namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport
{
    public class GetFireRiskAppraisalReportResponse
    {
        public FileResult FraewFile { get; set; }
        public FileResult SummaryFile { get; set; }
        public FileResult FraReportFile { get; set; }
    }

    public class FileResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
    }
}
