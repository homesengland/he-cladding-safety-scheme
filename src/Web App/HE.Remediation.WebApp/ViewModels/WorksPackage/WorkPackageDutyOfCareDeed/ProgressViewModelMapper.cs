using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDutyOfCareDeed.Progress.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageDutyOfCareDeed;

public class ProgressViewModelMapper : Profile
{
    public ProgressViewModelMapper()
    {
        CreateMap<GetProgressResponse, ProgressViewModel>();
    }
}
