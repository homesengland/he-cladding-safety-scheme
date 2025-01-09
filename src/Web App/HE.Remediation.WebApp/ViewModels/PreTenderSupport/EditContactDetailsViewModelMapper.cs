using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.SetConfirmContactDetails;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.EmailContactDetails;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class EditContactDetailsViewModelMapper : Profile
{
    public EditContactDetailsViewModelMapper()
    {
        CreateMap<GetEmailContactDetailsResponse, EditContactDetailsViewModel>();
        CreateMap<EditContactDetailsViewModel, SetEmailContactDetailsRequest>();
    }       
}
