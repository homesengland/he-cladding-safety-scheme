using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.GetReasonNoDesigner;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.SetReasonNoDesigner;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonNoDesignerViewModelMapper : Profile
{
    public ReasonNoDesignerViewModelMapper()
    {
        CreateMap<GetReasonNoDesignerResponse, ReasonNoDesignerViewModel>();
        CreateMap<ReasonNoDesignerViewModel, SetReasonNoDesignerRequest>();
    }
}
