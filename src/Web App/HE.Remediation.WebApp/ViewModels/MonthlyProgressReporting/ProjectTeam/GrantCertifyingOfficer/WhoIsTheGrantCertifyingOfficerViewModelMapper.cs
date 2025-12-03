using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class WhoIsTheGrantCertifyingOfficerViewModelMapper : Profile
{
    public WhoIsTheGrantCertifyingOfficerViewModelMapper()
    {
        CreateMap<GetWhoIsTheGrantCertifyingOfficerResponse, WhoIsTheGrantCertifyingOfficerViewModel>();
        CreateMap<GetWhoIsTheGrantCertifyingOfficerResponse.TeamMemberResponse, WhoIsTheGrantCertifyingOfficerViewModel.TeamMemberViewModel>();

        CreateMap<WhoIsTheGrantCertifyingOfficerViewModel, SetWhoIsTheGrantCertifyingOfficerRequest>();
    }
}