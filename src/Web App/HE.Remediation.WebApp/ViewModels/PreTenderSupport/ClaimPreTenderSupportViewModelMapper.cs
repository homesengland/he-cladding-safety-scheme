using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ClaimPreTender.GetClaimPretenderSupport;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class ClaimPreTenderSupportViewModelMapper: Profile
{
    public ClaimPreTenderSupportViewModelMapper()
    {
        CreateMap<GetClaimPretenderSupportResponse , ClaimPreTenderSupportViewModel>();
    }
}
