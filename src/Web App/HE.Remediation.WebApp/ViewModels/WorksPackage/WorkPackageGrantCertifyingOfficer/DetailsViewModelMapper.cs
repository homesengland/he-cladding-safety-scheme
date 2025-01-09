using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class DetailsViewModelMapper : Profile
{
    public DetailsViewModelMapper()
    {
        CreateMap<GetDetailsResponse, DetailsViewModel>();
        CreateMap<DetailsViewModel, SetDetailsRequest>();
    }
}
