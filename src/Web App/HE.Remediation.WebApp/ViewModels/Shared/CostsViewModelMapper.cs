using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSubmitPayment;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;
using HE.Remediation.Core.UseCase.Shared.Costs.Set;

namespace HE.Remediation.WebApp.ViewModels.Shared;

public class CostsViewModelMapper : Profile
{
    public CostsViewModelMapper()
    {
        CreateMap<GetCostsResponse, CostsViewModel>()
                        .ForMember(d => d.PtfsPaymentText,
                       o => o.MapFrom(s => s.PtfsPayment.HasValue
                                             ? s.PtfsPayment.Value.ToString("N0")
                                             : null));
        CreateMap<CostsViewModel, SetCostsRequest>();

        CreateMap<GetSubmitPaymentResponse, CostsViewModel>();

        CreateMap<CostsViewModel, SetSubmitPaymentRequest>();

        CreateMap<SetSubmitPaymentRequest, CostsViewModel>();
    }
}
