using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.GetConfirmContactDetails;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class ConfirmContactDetailsViewModelMapper: Profile
{
    public ConfirmContactDetailsViewModelMapper()
    {
        CreateMap<GetConfirmContactDetailsResponse, ConfirmContactDetailsViewModel>();
    }
}
