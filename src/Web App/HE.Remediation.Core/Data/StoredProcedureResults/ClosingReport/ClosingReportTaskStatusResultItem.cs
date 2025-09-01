using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport
{
    public class ClosingReportTaskStatusResultItem
    {
        public EClosingReportTask ClosingReportTask { get; set; }
        public ETaskStatus TaskStatus { get; set; }
    }
}
