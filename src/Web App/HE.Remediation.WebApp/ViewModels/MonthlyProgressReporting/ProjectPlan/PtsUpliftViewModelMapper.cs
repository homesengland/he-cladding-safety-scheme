using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan;

public class PtsUpliftViewModelMapper : Profile
{
    public PtsUpliftViewModelMapper()
    {
        CreateMap<GetPtsUpliftResponse, PtsUpliftViewModel>();
        CreateMap<PtsUpliftViewModel, SetPtsUpliftRequest>();
    }
}