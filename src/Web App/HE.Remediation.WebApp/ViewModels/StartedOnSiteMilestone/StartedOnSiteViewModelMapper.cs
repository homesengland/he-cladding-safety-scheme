using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.StartedOnSiteMilestone;

namespace HE.Remediation.WebApp.ViewModels.StartedOnSiteMilestone;

public class StartedOnSiteViewModelMapper : Profile
{
    public StartedOnSiteViewModelMapper()
    {
        CreateMap<GetStartedOnSiteResponse, StartedOnSiteViewModel>();
        CreateMap<StartedOnSiteViewModel, UpdateStartedOnSiteRequest>();
    }   
}