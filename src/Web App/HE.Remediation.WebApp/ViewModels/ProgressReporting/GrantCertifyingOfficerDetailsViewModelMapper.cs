using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class GrantCertifyingOfficerDetailsViewModelMapper : Profile
{
    public GrantCertifyingOfficerDetailsViewModelMapper()
    {
        CreateMap<GetGrantCertifyingOfficerDetailsResponse, GrantCertifyingOfficerDetailsViewModel>();
        CreateMap<GrantCertifyingOfficerDetailsViewModel, SetGrantCertifyingOfficerDetailsRequest>();
    }
}