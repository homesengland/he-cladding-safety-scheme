using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetReasonForClosing;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.SetReasonForClosing;

namespace HE.Remediation.WebApp.ViewModels.WithdrawalRequest
{
    public class ReasonForClosingViewModelMapper : Profile
    {
        public ReasonForClosingViewModelMapper()
        {
            CreateMap<GetReasonForClosingResponse, ReasonForClosingViewModel>();
            CreateMap<ReasonForClosingViewModel, SetReasonForClosingRequest>();
        }
    }
}