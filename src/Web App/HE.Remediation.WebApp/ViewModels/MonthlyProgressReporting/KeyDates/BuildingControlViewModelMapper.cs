using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class BuildingControlViewModelMapper : Profile
{
    public BuildingControlViewModelMapper()
    {
        CreateMap<GetBuildingControlResponse, BuildingControlViewModel>();
        CreateMap<BuildingControlViewModel, SetBuildingControlRequest>();
    }
}
