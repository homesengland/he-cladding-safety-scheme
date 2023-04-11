using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class LeaseholderOrPrivateOwnerViewModelMapper : Profile
{
    public LeaseholderOrPrivateOwnerViewModelMapper()
    {
        CreateMap<GetLeaseholderOrPrivateOwnerResponse, LeaseholderOrPrivateOwnerViewModel>();
        CreateMap<LeaseholderOrPrivateOwnerViewModel, SetLeaseholderOrPrivateOwnerRequest>();
    }
}