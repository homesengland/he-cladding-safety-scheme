using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class DeclarationViewModelMapper : Profile
{
    public DeclarationViewModelMapper()
    {
        CreateMap<GetDeclarationResponse, DeclarationViewModel>();
        CreateMap<DeclarationViewModel, SetDeclarationRequest>();
    }
}
