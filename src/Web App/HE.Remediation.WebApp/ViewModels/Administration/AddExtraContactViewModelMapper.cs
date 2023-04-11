using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.GetExtraContact;
using HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.SetExtraContact;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class AddExtraContactViewModelMapper: Profile
{
    public AddExtraContactViewModelMapper()
    {        
        CreateMap<GetExtraContactResponse, AddExtraContactViewModel>();
        CreateMap<AddExtraContactViewModel, SetExtraContactRequest>();
    }    
}
