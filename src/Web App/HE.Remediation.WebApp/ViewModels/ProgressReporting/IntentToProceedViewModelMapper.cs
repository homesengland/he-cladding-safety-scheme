using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class IntentToProceedViewModelMapper : Profile
{
    public IntentToProceedViewModelMapper()
    {
        CreateMap<GetIntentToProceedResponse, IntentToProceedViewModel>();
        CreateMap<IntentToProceedViewModel, SetIntentToProceedRequest>();
    }
}