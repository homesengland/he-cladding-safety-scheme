using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.Costs;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class CostsViewModelMapper: Profile
{
    public CostsViewModelMapper()
    {
        CreateMap<GetSubmitPaymentResponse, CostsViewModel>();        
        CreateMap<MonthlyCost, MonthlyCostViewModel>()
            .ForMember(x=> x.AmountText, opt=> opt.MapFrom(s => s.Amount.HasValue
                                             ? s.Amount.Value.ToString("N0")
                                             : null));
        CreateMap<MonthlyCostResult, MonthlyCostViewModel>();
        CreateMap<MonthlyCostViewModel, MonthlyCost>();
        CreateMap<CostsViewModel, SetSubmitPaymentRequest>();
    }
}
