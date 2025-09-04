using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class OtherPartiesViewModelMapper : Profile
{
    public OtherPartiesViewModelMapper()
    {
        CreateMap<GetOtherPartiesResponse, OtherPartiesViewModel>();
        CreateMap<OtherPartiesViewModel, SetOtherPartiesRequest>();
    }
}