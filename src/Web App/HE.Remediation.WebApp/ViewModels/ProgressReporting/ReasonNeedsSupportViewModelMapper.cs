using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.GetReasonNeedsSupport;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.SetReasonNeedsSupport;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonNeedsSupportViewModelMapper : Profile
{
    public ReasonNeedsSupportViewModelMapper()
    {
        CreateMap<GetReasonNeedsSupportResponse, ReasonNeedsSupportViewModel>();
        CreateMap<ReasonNeedsSupportViewModel, SetReasonNeedsSupportRequest>();
    }
}
