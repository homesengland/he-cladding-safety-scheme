using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class ProvideBuildingAddressViewModelMapper : Profile
    {
        public ProvideBuildingAddressViewModelMapper()
        {
            CreateMap<ProvideBuildingAddressViewModel, SetBuildingAddressRequest>();
            CreateMap<GetBuildingAddressResponse, ProvideBuildingAddressViewModel>();
        }
    }
}