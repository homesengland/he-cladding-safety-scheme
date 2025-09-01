using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class ReportViewModelMapper : Profile
{
    public ReportViewModelMapper()
    {
        CreateMap<GetReportResponse, ReportViewModel>();
        CreateMap<ReportViewModel, SetReportRequest>();
    }
}