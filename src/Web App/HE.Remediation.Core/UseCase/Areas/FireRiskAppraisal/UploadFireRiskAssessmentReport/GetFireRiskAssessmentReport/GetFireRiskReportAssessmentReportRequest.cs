﻿using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport;

public class GetFireRiskReportAssessmentReportRequest : IRequest<GetFireRiskAssessmentReportResponse>
{
    private GetFireRiskReportAssessmentReportRequest()
    {
    }

    public static readonly GetFireRiskReportAssessmentReportRequest Request = new();
}