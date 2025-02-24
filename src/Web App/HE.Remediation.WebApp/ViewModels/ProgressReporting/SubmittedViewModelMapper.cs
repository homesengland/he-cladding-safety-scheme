using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Submitted.GetSubmitted;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class SubmittedViewModelMapper : Profile
    {
        public SubmittedViewModelMapper()
        {
            CreateMap<GetSubmittedResponse, SubmittedViewModel>();
        }
    }
}
