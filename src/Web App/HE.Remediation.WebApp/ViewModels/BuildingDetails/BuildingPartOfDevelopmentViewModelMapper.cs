using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.SetBuildingPartOfDevelopment;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class BuildingPartOfDevelopmentViewModelMapper : Profile
    {
        public BuildingPartOfDevelopmentViewModelMapper()
        {
            CreateMap<BuildingPartOfDevelopmentViewModel, SetBuildingPartOfDevelopmentRequest>();
            CreateMap<GetBuildingPartOfDevelopmentResponse, BuildingPartOfDevelopmentViewModel>();
        }
    }
}