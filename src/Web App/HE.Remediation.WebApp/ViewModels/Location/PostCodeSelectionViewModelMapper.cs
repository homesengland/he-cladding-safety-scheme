using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUserManual;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddressManual;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using HE.Remediation.WebApp.ViewModels.Administration;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;
using GrantCertifyingOfficerAddressDetails = HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeSelectionViewModelMapper : Profile
{
    public PostCodeSelectionViewModelMapper()
    {
        CreateMap<PostCodeSelectionViewModel, ProvideBuildingAddressViewModel>();
        CreateMap<ProvideBuildingAddressViewModel, PostCodeSelectionViewModel>();
        CreateMap<PostCodeSelectionViewModel, PostCodeResultsViewModel>();
        CreateMap<PostCodeSelectionViewModel, SetBuildingAddressRequest>();
        CreateMap<PostCodeSelectionViewModel, SetCorrespondenceAddressRequest>();
        CreateMap<CorrespondenceAddressViewModel, SetCorrespondenceAddressManualRequest>();
        CreateMap<CorrespondenceAddressViewModel, PostCodeEntryViewModel>();
        CreateMap<PostCodeManualViewModel, SetCompanyAddressForCurrentUserManualRequest>();
        CreateMap<PostCodeSelectionViewModel, SetResponsibleEntityCompanyAddressRequest>();
        CreateMap<PostCodeSelectionViewModel, SetFreeholderAddressRequest>();
        CreateMap<GrantCertifyingOfficerAddressDetails.Get.GetAddressDetailsResponse, PostCodeSelectionViewModel>();
        CreateMap<PostCodeSelectionViewModel, GrantCertifyingOfficerAddressDetails.Set.SetAddressDetailsRequest>();
        CreateMap<PostCodeSelectionViewModel, SetGrantCertifyingOfficerAddressResultRequest>();
    }
}
