using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class VariationReasonViewModelMapper : Profile
{
    public VariationReasonViewModelMapper()
    {
        CreateMap<GetVariationReasonResponse, VariationReasonViewModel>();
        CreateMap<VariationReasonViewModel, SetVariationReasonRequest>();
    }
}
