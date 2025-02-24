using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class ConfirmViewModelMapper : Profile
{
    public ConfirmViewModelMapper()
    {
        CreateMap<GetConfirmResponse, ConfirmViewModel>();
        CreateMap<ConfirmViewModel, SetConfirmRequest>();
    }
}
