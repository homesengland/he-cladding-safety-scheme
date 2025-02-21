using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlValidationViewModelMapper : Profile
{
    public BuildingControlValidationViewModelMapper()
    {
        CreateMap<GetBuildingControlValidationResponse, BuildingControlValidationViewModel>();
        CreateMap<BuildingControlValidationViewModel, SetBuildingControlValidationRequest>();
    }
}