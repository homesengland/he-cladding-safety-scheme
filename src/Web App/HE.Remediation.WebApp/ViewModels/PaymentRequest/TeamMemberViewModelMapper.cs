using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetTeamMember;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.UpdateTeamMember;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class TeamMemberViewModelMapper : Profile
{
    public TeamMemberViewModelMapper()
    {
        CreateMap<GetTeamMemberResponse, TeamMemberViewModel>();
        CreateMap<TeamMemberViewModel, UpdateTeamMemberRequest>();         
    }
}
