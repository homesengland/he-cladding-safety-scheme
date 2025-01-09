using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SummariseProgress;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class SummariseProgressViewModelMapper : Profile
    {
        public SummariseProgressViewModelMapper()
        {
            CreateMap<GetSummariseProgressResponse, SummariseProgressViewModel>();
            CreateMap<SummariseProgressViewModel, SetSummariseProgressRequest>();
        }
    }
}
