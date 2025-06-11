using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class UploadFireRiskAssessmentReportViewModelMapper : Profile
{
    public UploadFireRiskAssessmentReportViewModelMapper()
    {
        CreateMap<GetFireRiskAssessmentReportResponse, UploadFireRiskAssessmentReportViewModel>();
    }
}