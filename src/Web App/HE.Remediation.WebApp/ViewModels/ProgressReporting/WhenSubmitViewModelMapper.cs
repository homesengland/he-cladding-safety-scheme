using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.GetWhenSubmit;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhenSubmitViewModelMapper : Profile
{
    public WhenSubmitViewModelMapper()
    {
        CreateMap<GetWhenSubmitResponse, WhenSubmitViewModel>();
        CreateMap<WhenSubmitViewModel, SetWhenSubmitRequest>();
    }
}
