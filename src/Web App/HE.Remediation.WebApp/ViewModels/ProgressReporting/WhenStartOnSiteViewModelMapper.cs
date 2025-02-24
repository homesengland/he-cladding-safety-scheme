using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.GetWhenStartOnSite;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.SetWhenStartOnSite;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.GetWhenSubmit;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhenStartOnSiteViewModelMapper : Profile
{
    public WhenStartOnSiteViewModelMapper()
    {
        CreateMap<GetWhenStartOnSiteResponse, WhenStartOnSiteViewModel>();
        CreateMap<WhenStartOnSiteViewModel, SetWhenStartOnSiteRequest>();
    }
}
