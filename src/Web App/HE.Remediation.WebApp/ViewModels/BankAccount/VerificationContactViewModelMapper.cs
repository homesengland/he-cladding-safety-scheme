using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetVerificationContact;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetVerificationContact;

namespace HE.Remediation.WebApp.ViewModels.BankAccount;

public class VerificationContactViewModelMapper : Profile
{
    public VerificationContactViewModelMapper()
    {
        CreateMap<GetVerificationContactResponse, VerificationContactViewModel>();
        CreateMap<VerificationContactViewModel, SetVerificationContactRequest>();
    }
}