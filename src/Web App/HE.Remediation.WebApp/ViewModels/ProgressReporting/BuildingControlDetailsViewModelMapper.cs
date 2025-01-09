using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.GetBuildingControlDetails;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.UpdateBuildingControlDetails;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class BuildingControlDetailsViewModelMapper : Profile
    {
        public BuildingControlDetailsViewModelMapper()
        {
            CreateMap<GetBuildingControlDetailsResponse, BuildingControlDetailsViewModel>();
            CreateMap<BuildingControlDetailsViewModel, UpdateBuildingControlDetailsRequest>();
        }
    }
}
