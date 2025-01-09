using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.CostProfile.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class CostProfileViewModelMapper : Profile
{
    public CostProfileViewModelMapper()
    {
        CreateMap<GetCostProfileResponse, CostProfileViewModel>();
    }
}
