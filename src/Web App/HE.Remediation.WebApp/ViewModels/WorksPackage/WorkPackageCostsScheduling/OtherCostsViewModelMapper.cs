using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Other;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class OtherCostsViewModelMapper : Profile
{
    public OtherCostsViewModelMapper()
    {
        CreateMap<GetOtherCostsResponse, OtherCostsViewModel>()
            .ForMember(d => d.FraewSurveyAmountText, o => o.MapFrom(s => s.FraewSurveyAmount.HasValue ? s.FraewSurveyAmount.Value.ToString("N0") : null))
            .ForMember(d => d.FeasibilityStageAmountText, o => o.MapFrom(s => s.FeasibilityStageAmount.HasValue ? s.FeasibilityStageAmount.Value.ToString("N0") : null))
            .ForMember(d => d.PostTenderStageAmountText, o => o.MapFrom(s => s.PostTenderStageAmount.HasValue ? s.PostTenderStageAmount.Value.ToString("N0") : null))
            .ForMember(d => d.PropertyManagerAmountText, o => o.MapFrom(s => s.PropertyManagerAmount.HasValue ? s.PropertyManagerAmount.Value.ToString("N0") : null))
            .ForMember(d => d.IrrecoverableVatAmountText, o => o.MapFrom(s => s.IrrecoverableVatAmount.HasValue ? s.IrrecoverableVatAmount.Value.ToString("N0") : null));
        CreateMap<OtherCostsViewModel, SetOtherCostsRequest>();
    }
}