using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetDeclaration;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetDeclaration;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class DeclarationViewModelMapper : Profile
{
    public DeclarationViewModelMapper()
    {
        CreateMap<GetDeclarationResponse, DeclarationViewModel>();
        CreateMap<DeclarationViewModel, SetDeclarationRequest>();
    }
}
