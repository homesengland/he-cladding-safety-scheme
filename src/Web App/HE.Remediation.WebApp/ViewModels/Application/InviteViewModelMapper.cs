using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class InviteViewModelMapper : Profile
    {
        public InviteViewModelMapper()
        {
            CreateMap<GetInviteResponse, InviteViewModel>();
        }
    }
}
