using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperAddressInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperAddressInformation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationAddressViewModelMapper : Profile
{
    public BuildingDeveloperInformationAddressViewModelMapper ()
    {
        CreateMap<BuildingDeveloperInformationAddressViewModel, SetBuildingDeveloperInformationAddressRequest>();
        CreateMap<GetBuildingDeveloperInformationAddressResponse, BuildingDeveloperInformationAddressViewModel>();
    }
}
