using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetReviewPayment;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ReviewPaymentRequestViewModelMapper : Profile
{
    public ReviewPaymentRequestViewModelMapper()
    {
        CreateMap<GetReviewPaymentResponse, ReviewPaymentRequestViewModel>();
        CreateMap<ReviewPaymentRequestViewModel, SetReviewPaymentRequest>();
    }
}
