using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ThirdPartyViewModelMapper : Profile
    {
        public ThirdPartyViewModelMapper()
        {
            CreateMap<GetThirdPartyResponse, ThirdPartyViewModel>();
            CreateMap<GetThirdPartyResponse.TeamMember, ThirdPartyViewModel.TeamMember>();
        }
    }
}
