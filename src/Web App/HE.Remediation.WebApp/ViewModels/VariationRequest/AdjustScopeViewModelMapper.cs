using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class AdjustScopeViewModelMapper : Profile
    {
        public AdjustScopeViewModelMapper()
        {
            CreateMap<GetAdjustScopeResponse, AdjustScopeViewModel>();
            CreateMap<AdjustScopeViewModel, SetAdjustScopeRequest>();
        }
    }
}
