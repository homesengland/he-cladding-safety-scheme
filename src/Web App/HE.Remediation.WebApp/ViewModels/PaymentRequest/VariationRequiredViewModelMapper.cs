using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetVariationRequired;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class VariationRequiredViewModelMapper : Profile
{
    public VariationRequiredViewModelMapper()
    {
        CreateMap<GetVariationRequiredResponse, VariationRequiredViewModel>();
    }
}
