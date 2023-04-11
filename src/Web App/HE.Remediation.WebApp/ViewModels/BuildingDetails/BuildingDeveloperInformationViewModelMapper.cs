using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperInformation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationViewModelMapper : Profile
{
    public BuildingDeveloperInformationViewModelMapper()
    {
        CreateMap<BuildingDeveloperInformationViewModel, SetBuildingDeveloperInformationRequest>();

        CreateMap<GetBuildingDeveloperInformationResponse, BuildingDeveloperInformationViewModel>();
    }
}