using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.GetUserContactConsent;
using HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.SetUserContactConsent;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class ContactInfoConsentViewModelMapper: Profile
{    
    public ContactInfoConsentViewModelMapper()
    {
        CreateMap<ContactInfoConsentViewModel, SetUserContactConsentRequest>();
        CreateMap<GetUserContactConsentResponse, ContactInfoConsentViewModel> ();  
    }    
}
