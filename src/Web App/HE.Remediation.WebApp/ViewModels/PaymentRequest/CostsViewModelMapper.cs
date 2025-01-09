using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.Costs;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSubmitPayment;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest
{
    public class CostsViewModelMapper: Profile
    {
        public CostsViewModelMapper()
        {
            CreateMap<GetSubmitPaymentResponse, CostsViewModel>();

            CreateMap<CostsViewModel, SetSubmitPaymentRequest>();

            CreateMap<MonthlyCost, MonthlyCostViewModel>();

            CreateMap<MonthlyCostResult, MonthlyCostViewModel>();

            CreateMap<MonthlyCostViewModel, MonthlyCost>();
        }
    }
}
