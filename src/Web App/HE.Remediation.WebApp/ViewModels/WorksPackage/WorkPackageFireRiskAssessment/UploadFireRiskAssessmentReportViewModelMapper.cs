using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class UploadFireRiskAssessmentReportViewModelMapper : Profile
{
    public UploadFireRiskAssessmentReportViewModelMapper()
    {
        CreateMap<GetUploadFireRiskAssessmentReportResponse, UploadFireRiskAssessmentReportViewModel>();
        CreateMap<UploadFireRiskAssessmentReportViewModel, SetUploadFireRiskAssessmentReportRequest>();
    }
}