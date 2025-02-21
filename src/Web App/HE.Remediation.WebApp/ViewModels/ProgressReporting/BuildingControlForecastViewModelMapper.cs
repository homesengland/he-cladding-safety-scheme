using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlForecastViewModelMapper : Profile
{
    public BuildingControlForecastViewModelMapper()
    {
        CreateMap<GetBuildingControlForecastResponse, BuildingControlForecastViewModel>();
        CreateMap<BuildingControlForecastViewModel, SetBuildingControlForecastRequest>();
    }
}