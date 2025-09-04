using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class RoleForRemediationContributionViewModelMapper : Profile
{
    public RoleForRemediationContributionViewModelMapper()
    {
        CreateMap<GetRoleForRemediationContributionResponse, RoleForRemediationContributionViewModel>();
        CreateMap<RoleForRemediationContributionViewModel, SetRoleForRemediationContributionRequest>();
    }
}