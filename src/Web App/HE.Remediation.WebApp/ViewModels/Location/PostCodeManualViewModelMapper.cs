using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddressManual;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddressManual;
using HE.Remediation.Core.UseCase.Areas.Location.BuildingLookup;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using ProgressReportings = HE.Remediation.Core.UseCase.Areas.ProgressReporting;
using ProgressReportingsV2 = HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using GrantCertifyingOfficerAddressDetails = HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails;


namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeManualViewModelMapper: Profile
{
    public PostCodeManualViewModelMapper()
    {
        CreateMap<GetBuildingAddressResponse, PostCodeManualViewModel>();
        CreateMap<BuildingLookupResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeManualViewModel, SetBuildingAddressManualRequest>();
        CreateMap<PostCodeManualViewModel, SetCorrespondenceAddressManualRequest>();
        CreateMap<PostCodeManualViewModel, SetRepresentationCompanyOrIndividualAddressDetailsRequest>();
        CreateMap<PostCodeManualViewModel, SetRepresentationCompanyOrIndividualAddressManualDetailsRequest>();        
        CreateMap<PostCodeManualViewModel, SetResponsibleEntityCompanyAddressManualRequest>();
        CreateMap<GetResponsibleEntityCompanyAddressResponse, PostCodeManualViewModel>();
        CreateMap<GetRepresentationCompanyOrIndividualAddressDetailsResponse, PostCodeManualViewModel>();
        CreateMap<GrantCertifyingOfficerAddressDetails.Get.GetAddressDetailsResponse, PostCodeManualViewModel>();
        CreateMap<GetPostCodeResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeManualViewModel,SetBuildingAddressManualRequest>();
        CreateMap<GetFreeholderAddressResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeManualViewModel, SetFreeholderAddressRequest>();
        CreateMap<PostCodeManualViewModel, SetFreeholderAddressManualRequest>();
        CreateMap<PostCodeManualViewModel, GrantCertifyingOfficerAddressDetails.SetManual.SetAddressManualDetailsRequest>();
        CreateMap<PostCodeManualViewModel, ProgressReportings.SetGrantCertifyingOfficerAddressRequest>();
        CreateMap<ProgressReportings.GetGrantCertifyingOfficerAddressResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeManualViewModel, ProgressReportingsV2.SetGrantCertifyingOfficerAddressRequest>();
        CreateMap<ProgressReportingsV2.GetGrantCertifyingOfficerAddressResponse, PostCodeManualViewModel>();
    }
}
