using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement;

public class InviteMemberViewModelMapper : Profile
{
    public InviteMemberViewModelMapper()
    {
        CreateMap<UpsertMemberResponse, InviteMemberViewModel>();
        CreateMap<InviteMemberViewModel, UpsertMemberRequest>();

        CreateMap<GetMemberResponse, InviteMemberViewModel>();
    }
}
