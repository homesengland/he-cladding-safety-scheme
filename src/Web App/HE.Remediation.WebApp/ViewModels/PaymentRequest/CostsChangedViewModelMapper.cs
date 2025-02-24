using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCostsChanged;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCostsChanged;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class CostsChangedViewModelMapper : Profile
{
    public CostsChangedViewModelMapper()
    {
        CreateMap<GetCostsChangedResponse, CostsChangedViewModel>();
        CreateMap<CostsChangedViewModel, SetCostsChangedRequest>();
    }   
}
