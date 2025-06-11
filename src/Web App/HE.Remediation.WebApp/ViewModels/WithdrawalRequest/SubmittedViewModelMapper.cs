using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetSubmitted;

namespace HE.Remediation.WebApp.ViewModels.WithdrawalRequest
{
    public class SubmittedViewModelMapper : Profile
    {
        public SubmittedViewModelMapper()
        {
            CreateMap<GetSubmittedResponse, SubmittedViewModel>();
        }
    }
}