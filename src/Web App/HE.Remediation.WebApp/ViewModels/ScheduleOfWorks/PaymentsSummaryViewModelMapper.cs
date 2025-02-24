using AutoMapper;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class PaymentsSummaryViewModelMapper : Profile
{
    public PaymentsSummaryViewModelMapper()
    {
        CreateMap<GetCostsResponse, PaymentsSummaryViewModel>()
            .ForMember(d => d.Costs, act => act.MapFrom(s => s));
    }
}
