using AutoMapper;
using HE.Remediation.Core.Services.Location;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeResultsViewModelMapper : Profile
{
    public PostCodeResultsViewModelMapper()
    {                
        CreateMap<PostCodeResults, GetPostCodeResponse>();      
        CreateMap<GetPostCodeResponse, ProvideBuildingAddressViewModel>();
        CreateMap<ParsedAddress, SetBuildingAddressRequest>();
    }
}
