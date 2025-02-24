using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.GetGrantFundingSignatories;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class GrantFundingSignatoriesViewModelMapper : Profile
{
    public GrantFundingSignatoriesViewModelMapper()
    {
        CreateMap<GetGrantFundingSignatoriesResponse, GrantFundingSignatoriesViewModel>();
    }
}