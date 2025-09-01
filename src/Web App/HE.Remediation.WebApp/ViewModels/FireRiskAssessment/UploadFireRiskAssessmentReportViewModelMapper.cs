using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class UploadFireRiskAssessmentReportViewModelMapper : Profile
{
    public UploadFireRiskAssessmentReportViewModelMapper()
    {
        CreateMap<GetFireRiskAssessmentReportResponse, UploadFireRiskAssessmentReportViewModel>();
    }
}