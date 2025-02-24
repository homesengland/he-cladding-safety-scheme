using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class GrantCertifyingOfficerSignatoriesViewModelMapper : Profile
{
    public GrantCertifyingOfficerSignatoriesViewModelMapper()
    {
        CreateMap<GetGrantCertifyingOfficerSignatoryResponse, GrantCertifyingOfficerSignatoriesViewModel>();
        CreateMap<GrantCertifyingOfficerSignatoriesViewModel, SetGrantCertifyingOfficerSignatoryRequest>();
    }   
}