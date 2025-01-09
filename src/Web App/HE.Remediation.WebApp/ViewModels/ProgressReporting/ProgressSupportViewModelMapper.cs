using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.GetProgressSupport;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.SetProgressSupport;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProgressSupportViewModelMapper : Profile
{
    public ProgressSupportViewModelMapper()
    {
        CreateMap<GetProgressSupportResponse, ProgressSupportViewModel>();
        CreateMap<ProgressSupportViewModel, SetProgressSupportRequest>();
    }
}