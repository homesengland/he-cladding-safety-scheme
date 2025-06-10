using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.RemoveMember;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement;

public class RemoveMemberViewModelMapper : Profile
{
    public RemoveMemberViewModelMapper()
    {
        CreateMap<RemoveMembersResponse, RemoveMemberViewModel>();
    }
}
