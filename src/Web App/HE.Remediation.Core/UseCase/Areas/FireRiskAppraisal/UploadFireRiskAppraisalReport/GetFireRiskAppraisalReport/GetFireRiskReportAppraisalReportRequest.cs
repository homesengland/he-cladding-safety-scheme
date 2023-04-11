using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport
{
    public class GetFireRiskReportAppraisalReportRequest: IRequest<IReadOnlyCollection<GetFireRiskAppraisalReportResponse>>
    {
    }
}
