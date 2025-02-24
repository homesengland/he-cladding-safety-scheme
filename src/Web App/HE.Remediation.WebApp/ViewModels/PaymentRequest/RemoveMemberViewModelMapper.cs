using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.DeleteTeamMember;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetRemoveTeamMember;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class RemoveMemberViewModelMapper : Profile
{
    public RemoveMemberViewModelMapper()
    {
        CreateMap<GetRemoveTeamMemberResponse, RemoveMemberViewModel>();
        CreateMap<RemoveMemberViewModel, DeleteTeamMemberRequest>();
    }
}
