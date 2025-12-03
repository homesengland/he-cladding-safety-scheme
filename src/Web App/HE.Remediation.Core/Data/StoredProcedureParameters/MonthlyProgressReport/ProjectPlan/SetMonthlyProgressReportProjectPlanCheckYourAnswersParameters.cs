using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan
{
    public class SetMonthlyProgressReportProjectPlanCheckYourAnswersParameters
    {
        public Guid ApplicationId { get; set; }
        public Guid ProgressReportId { get; set; }
        public int TaskStatusId { get; set; }
    }
}
