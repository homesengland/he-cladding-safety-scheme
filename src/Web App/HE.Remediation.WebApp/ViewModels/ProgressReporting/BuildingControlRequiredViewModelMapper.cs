using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.GetBuildingControlRequired;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.UpdateBuildingControlRequired;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class BuildingControlRequiredViewModelMapper : Profile
    {
        public BuildingControlRequiredViewModelMapper()
        {
            CreateMap<GetBuildingControlRequiredResponse, BuildingControlRequiredViewModel>();
            CreateMap<BuildingControlRequiredViewModel, UpdateBuildingControlRequiredRequest>();
        }
    }
}
