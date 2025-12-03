using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport;

public class ProgressSupportTypeViewModelMapper : Profile
{
    public ProgressSupportTypeViewModelMapper()
    {
        CreateMap<GetProgressSupportTypeResponse, ProgressSupportTypeViewModel>();
        CreateMap<ProgressSupportTypeViewModel, SetProgressSupportTypeRequest>();
    }
}
