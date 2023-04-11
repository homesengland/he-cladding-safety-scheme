using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.AdditionalContacts.GetAdditionalContact;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class AdditionalContactViewModelValidator : Profile
{
    public AdditionalContactViewModelValidator()
    {            
        CreateMap<GetAdditionalContactResponse , AdditionalContactsViewModel.SecondaryContactDetails>();            
        //CreateMap<AdminSecondaryContactDetailsViewModel, SetSecondaryContactDetailsRequest>();            
    }
} 

 