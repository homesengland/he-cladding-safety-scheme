using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetDeclaration;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetDeclaration;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class DeclarationViewModelMapper : Profile
{
    public DeclarationViewModelMapper()
    {
        CreateMap<GetDeclarationResponse, DeclarationViewModel>();        
        CreateMap<DeclarationViewModel, SetDeclarationRequest>();        
    }
}
