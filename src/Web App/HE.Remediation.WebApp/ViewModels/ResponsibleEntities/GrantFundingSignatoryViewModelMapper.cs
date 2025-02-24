using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.GetGrantFundingSignatories;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class GrantFundingSignatoryViewModelMapper : Profile
{
    public GrantFundingSignatoryViewModelMapper()
    {
        CreateMap<GetGrantFundingSignatoryResponse, GrantFundingSignatoryViewModel>();
    }
}