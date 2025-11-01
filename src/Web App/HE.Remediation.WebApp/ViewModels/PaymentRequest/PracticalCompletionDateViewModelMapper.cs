using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPracticalCompletionDate;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetPracticalCompletionDate;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class PracticalCompletionDateViewModelMapper : Profile
{
    public PracticalCompletionDateViewModelMapper()
    {
        CreateMap<GetPracticalCompletionDateResponse, PracticalCompletionDateViewModel>();
        CreateMap<PracticalCompletionDateViewModel, SetPracticalCompletionDateRequest>();
    }
}