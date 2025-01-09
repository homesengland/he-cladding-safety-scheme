using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.HaveAnyAnswersChanged;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class HaveAnyAnswersChangedViewModelMapper : Profile
{
    public HaveAnyAnswersChangedViewModelMapper()
    {
        CreateMap<GetHaveAnyAnswersChangedResponse, HaveAnyAnswersChangedViewModel>();
    }
}