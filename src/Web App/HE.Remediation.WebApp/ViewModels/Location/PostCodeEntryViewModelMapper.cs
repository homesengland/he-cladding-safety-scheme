using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.GetCompanyAddressForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.Location.BuildingLookup;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using GrantCertifyingOfficerAddressDetails = HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;
using GrantCertifyingOfficerAddressDetailsV1 = HE.Remediation.Core.UseCase.Areas.ProgressReporting;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeEntryViewModelMapper: Profile
{
    public PostCodeEntryViewModelMapper()
    {                
        CreateMap<GetBuildingAddressResponse, PostCodeEntryViewModel>();                       
        CreateMap<GetPostCodeResponse, PostCodeSelectionViewModel>();
        CreateMap<BuildingLookupResponse, PostCodeSelectionViewModel>();
        CreateMap<PostCodeSelectionViewModel, ProvideBuildingAddressViewModel>();                         

        CreateMap<GetCompanyAddressForCurrentUserResponse, PostCodeEntryViewModel>();
        CreateMap<GetCorrespondenceAddressResponse, PostCodeEntryViewModel>();

        CreateMap<GetRepresentationCompanyOrIndividualAddressDetailsResponse, PostCodeEntryViewModel>();
        CreateMap<PostCodeSelectionViewModel, SetRepresentationCompanyOrIndividualAddressDetailsRequest>();
        
        CreateMap<GetFreeholderAddressResponse, PostCodeEntryViewModel>();

        CreateMap<GetResponsibleEntityCompanyAddressResponse, PostCodeEntryViewModel>();

        CreateMap<GrantCertifyingOfficerAddressDetails.Get.GetAddressDetailsResponse, PostCodeEntryViewModel>();

        CreateMap<GrantCertifyingOfficerAddressDetailsV1.GetGrantCertifyingOfficerAddressResponse, PostCodeEntryViewModel>();

        CreateMap<GetGrantCertifyingOfficerAddressResponse, PostCodeEntryViewModel>();
    }
}
