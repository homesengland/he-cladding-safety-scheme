using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.UserOnboarding;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement;

public class JoinViewModelMapper : Profile
{
    public JoinViewModelMapper()
    {
        CreateMap<GetOrgUserInviteResponse, JoinViewModel>();
    }
}
