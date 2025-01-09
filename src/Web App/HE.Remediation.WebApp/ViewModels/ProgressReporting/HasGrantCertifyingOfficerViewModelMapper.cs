using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class HasGrantCertifyingOfficerViewModelMapper : Profile
{
    public HasGrantCertifyingOfficerViewModelMapper()
    {
        CreateMap<GetHasGrantCertifyingOfficerResponse, HasGrantCertifyingOfficerViewModel>();
        CreateMap<HasGrantCertifyingOfficerViewModel, SetHasGrantCertifyingOfficerRequest>();
    }
}