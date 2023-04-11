using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.GetCompanyAddressForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeEntryViewModelMapper: Profile
{
    public PostCodeEntryViewModelMapper()
    {                
        CreateMap<GetBuildingAddressResponse, PostCodeEntryViewModel>();                       
        CreateMap<GetPostCodeResponse, PostCodeSelectionViewModel>();     
        CreateMap<PostCodeSelectionViewModel, ProvideBuildingAddressViewModel>();                         

        CreateMap<GetCompanyAddressForCurrentUserResponse, PostCodeEntryViewModel>();
        CreateMap<GetCorrespondenceAddressResponse, PostCodeEntryViewModel>();

        CreateMap<GetRepresentationCompanyOrIndividualAddressDetailsResponse, PostCodeEntryViewModel>();
        CreateMap<PostCodeSelectionViewModel, SetRepresentationCompanyOrIndividualAddressDetailsRequest>();
        
        CreateMap<GetFreeholderAddressResponse, PostCodeEntryViewModel>();

        CreateMap<GetResponsibleEntityCompanyAddressResponse, PostCodeEntryViewModel>();
    }
}
