using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class HaveYouAppliedForBuildingControlViewModelMapper : Profile
{
    public HaveYouAppliedForBuildingControlViewModelMapper()
    {
        CreateMap<GetHasAppliedForBuildingControlResponse, HaveYouAppliedForBuildingControlViewModel>();
        CreateMap<HaveYouAppliedForBuildingControlViewModel, SetHasAppliedForBuildingControlRequest>();
    }
}