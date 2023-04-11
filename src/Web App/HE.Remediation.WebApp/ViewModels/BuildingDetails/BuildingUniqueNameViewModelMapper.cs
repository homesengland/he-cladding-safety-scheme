using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.SetBuildingUniqueName;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class BuildingUniqueNameViewModelMapper : Profile
    {
        public BuildingUniqueNameViewModelMapper()
        {
            CreateMap<BuildingUniqueNameViewModel, SetBuildingUniqueNameRequest>();
            CreateMap<GetBuildingUniqueNameResponse, BuildingUniqueNameViewModel>();
        }
    }
}
