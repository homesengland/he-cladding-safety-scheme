using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Set;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class DeclarationViewModelMapper : Profile
{
    public DeclarationViewModelMapper()
    {
        CreateMap<GetDeclarationResponse, DeclarationViewModel>();
        CreateMap<DeclarationViewModel, SetDeclarationRequest>();
    }
}
