using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageDeclaration;

public class ConfirmViewModelMapper : Profile
{
    public ConfirmViewModelMapper()
    {
        CreateMap<GetDeclarationResponse, ConfirmViewModel>();
        CreateMap<ConfirmViewModel, SetDeclarationRequest>();
    }
}
