using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlDecisionViewModelMapper : Profile
{
    public BuildingControlDecisionViewModelMapper()
    {
        CreateMap<GetBuildingControlDecisionResponse, BuildingControlDecisionViewModel>();
        CreateMap<BuildingControlDecisionViewModel, SetBuildingControlDecisionRequest>();
    }
}