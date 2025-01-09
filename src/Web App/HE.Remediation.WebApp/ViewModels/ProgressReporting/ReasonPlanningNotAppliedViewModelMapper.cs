using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.GetReasonPlanningNotApplied;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.SetReasonPlanningNotApplied;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonPlanningNotAppliedViewModelMapper : Profile
{
    public ReasonPlanningNotAppliedViewModelMapper()
    {
        CreateMap<GetReasonPlanningNotAppliedResponse, ReasonPlanningNotAppliedViewModel>();
        CreateMap<ReasonPlanningNotAppliedViewModel, SetReasonPlanningNotAppliedRequest>();
    }
}
