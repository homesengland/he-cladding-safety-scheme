using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class SubmitPaymentRequestViewModelMapper : Profile
{
    public SubmitPaymentRequestViewModelMapper()
    {
        CreateMap<GetCostsResponse, SubmitPaymentRequestViewModel>()
            .ForMember(d => d.Costs, act => act.MapFrom(s => s));

        CreateMap<GetSubmitPaymentResponse, SubmitPaymentRequestViewModel>()
            .ForMember(d => d.Costs, act => act.MapFrom(s => s));

        CreateMap<MonthlyCost, MonthlyCostViewModel>()
            .ForMember(d => d.AmountText,
                       o => o.MapFrom(s => s.Amount.HasValue
                                             ? s.Amount.Value.ToString("N0")
                                             : null));
    }
}
