using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.GetGrantFundingSignatoryDetails;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.SetGrantFundingSignatoryDetails;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class GrantFundingSignatoryDetaislViewModelMapper : Profile
{
    public GrantFundingSignatoryDetaislViewModelMapper()
    {
        CreateMap<GetGrantFundingSignatoryDetailsResponse, GrantFundingSignatoryDetailsViewModel>();
        CreateMap<GrantFundingSignatoryDetailsViewModel, SetGrantFundingSignatoryDetailsRequest>();
    }
}