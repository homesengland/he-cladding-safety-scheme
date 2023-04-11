using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddressManual;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddressManual;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeManualViewModelMapper: Profile
{
    public PostCodeManualViewModelMapper()
    {
        CreateMap<GetBuildingAddressResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeManualViewModel, SetBuildingAddressManualRequest>();
        CreateMap<PostCodeManualViewModel, SetCorrespondenceAddressManualRequest>();
        CreateMap<PostCodeManualViewModel, SetRepresentationCompanyOrIndividualAddressDetailsRequest>();
        CreateMap<PostCodeManualViewModel, SetRepresentationCompanyOrIndividualAddressManualDetailsRequest>();        
        CreateMap<PostCodeManualViewModel, SetResponsibleEntityCompanyAddressManualRequest>();
        CreateMap<GetResponsibleEntityCompanyAddressResponse, PostCodeManualViewModel>();
        CreateMap<GetRepresentationCompanyOrIndividualAddressDetailsResponse, PostCodeManualViewModel>();
        CreateMap<GetPostCodeResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeManualViewModel,SetBuildingAddressManualRequest>();
        CreateMap<GetFreeholderAddressResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeManualViewModel, SetFreeholderAddressRequest>();
        CreateMap<PostCodeManualViewModel, SetFreeholderAddressManualRequest>();
    }
}
