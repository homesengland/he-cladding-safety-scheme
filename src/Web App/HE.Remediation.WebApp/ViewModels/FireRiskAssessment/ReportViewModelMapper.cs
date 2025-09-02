using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class ReportViewModelMapper : Profile
{
    public ReportViewModelMapper()
    {
        CreateMap<GetReportResponse, ReportViewModel>();
        CreateMap<ReportViewModel, SetReportRequest>();
    }
}