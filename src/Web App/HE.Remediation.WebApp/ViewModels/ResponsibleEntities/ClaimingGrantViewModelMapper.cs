using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ClaimingGrantViewModelMapper : Profile
{
    public ClaimingGrantViewModelMapper()
    {
        CreateMap<GetIsClaimingGrantResponse, ClaimingGrantViewModel>();
        CreateMap<ClaimingGrantViewModel, SetIsClaimingGrantRequest>();
    }
}