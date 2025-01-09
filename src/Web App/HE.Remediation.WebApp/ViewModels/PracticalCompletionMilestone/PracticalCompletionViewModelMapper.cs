using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PracticalCompletionMilestone;

namespace HE.Remediation.WebApp.ViewModels.PracticalCompletionMilestone;

public class PracticalCompletionViewModelMapper : Profile
{
    public PracticalCompletionViewModelMapper()
    {
        CreateMap<GetPracticalCompletionResponse, PracticalCompletionViewModel>();
        CreateMap<PracticalCompletionViewModel, UpdatePracticalCompletionRequest>();
    }
}