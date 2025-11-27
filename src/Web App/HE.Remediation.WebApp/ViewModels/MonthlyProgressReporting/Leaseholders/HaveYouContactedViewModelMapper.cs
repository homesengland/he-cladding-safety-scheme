using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

public class HaveYouContactedViewModelMapper : Profile
{
    public HaveYouContactedViewModelMapper()
    {
        CreateMap<GetHaveYouContactedResponse, HaveYouContactedViewModel>();
        CreateMap<HaveYouContactedViewModel, SetHaveYouContactedRequest>();
    }
}
