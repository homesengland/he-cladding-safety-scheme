using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhoIsTheGrantCertifyingOfficerViewModelMapper : Profile
{
    public WhoIsTheGrantCertifyingOfficerViewModelMapper()
    {
        CreateMap<GetWhoIsTheGrantCertifyingOfficerResponse, WhoIsTheGrantCertifyingOfficerViewModel>();
        CreateMap<GetWhoIsTheGrantCertifyingOfficerResponse.TeamMemberResponse, WhoIsTheGrantCertifyingOfficerViewModel.TeamMemberViewModel>();

        CreateMap<WhoIsTheGrantCertifyingOfficerViewModel, SetWhoIsTheGrantCertifyingOfficerRequest>();
    }
}