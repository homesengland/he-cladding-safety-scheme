using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class BuildingControlDatesChangedViewModelMapper : Profile
{
    public BuildingControlDatesChangedViewModelMapper()
    {
        CreateMap<GetBuildingControlDatesChangedResponse, BuildingControlDatesChangedViewModel>();
        CreateMap<BuildingControlDatesChangedViewModel, SetBuildingControlDatesChangedRequest>();
    }
}