using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class AuthorisedSignatoriesViewModelMapper : Profile
{
    public AuthorisedSignatoriesViewModelMapper()
    {
        CreateMap<GetAuthorisedSignatoriesResponse, AuthorisedSignatoriesViewModel>();
        CreateMap<AuthorisedSignatoriesViewModel, SetAuthorisedSignatoriesRequest>();
    }
}
