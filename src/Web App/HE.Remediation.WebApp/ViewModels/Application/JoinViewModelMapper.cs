using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Join;

namespace HE.Remediation.WebApp.ViewModels.Application;

public class JoinViewModelMapper : Profile
{
    public JoinViewModelMapper()
    {
        CreateMap<GetThirdPartyJoinResponse, JoinViewModel>();
    }
}
