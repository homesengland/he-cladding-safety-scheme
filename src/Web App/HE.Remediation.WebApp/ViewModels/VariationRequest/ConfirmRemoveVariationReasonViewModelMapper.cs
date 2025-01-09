using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ConfirmRemoveVariationReason.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Set;
using HE.Remediation.WebApp.ViewModels.VariationRequest;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class ConfirmRemoveVariationReasonViewModelMapper : Profile
{
    public ConfirmRemoveVariationReasonViewModelMapper()
    {
        CreateMap<GetConfirmRemoveVariationReasonResponse, ConfirmRemoveVariationReasonViewModel>();
        CreateMap<VariationReasonViewModel, ConfirmRemoveVariationReasonViewModel>()
            .ForMember(d => d.IsCostVariation, o => o.MapFrom(s => s.IsCostVariation))
            .ForMember(d => d.IsScopeVariation, o => o.MapFrom(s => s.IsScopeVariation))
            .ForMember(d => d.IsTimescaleVariation, o => o.MapFrom(s => s.IsTimescaleVariation));
        CreateMap<ConfirmRemoveVariationReasonViewModel, SetVariationReasonRequest>();
    }
}
