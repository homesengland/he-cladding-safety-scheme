using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetReviewPayment;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class ReviewPaymentRequestViewModelMapper : Profile
{
    public ReviewPaymentRequestViewModelMapper()
    {
        CreateMap<GetReviewPaymentResponse, ReviewPaymentRequestViewModel>();
        CreateMap<ReviewPaymentRequestViewModel, SetReviewPaymentRequest>();
        CreateMap<GetBuildingsInsuranceResponse, ReviewPaymentRequestViewModel>();
    }
}
