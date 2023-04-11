using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetAccountGrantPaidTo;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsRepresentative;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsResponsibleEntity;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetAccountGrantPaidTo;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsRepresentative;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsResponsibleEntity;

namespace HE.Remediation.WebApp.ViewModels.BankAccount
{
    public class BankAccountDetailsViewModelMapper : Profile
    {
        public BankAccountDetailsViewModelMapper()
        {
            CreateMap<GetBankAccountDetailsResponsibleEntityResponse, BankAccountDetailsViewModel>();

            CreateMap<BankAccountDetailsViewModel, SetBankAccountDetailsResponsibleEntityRequest>();

            CreateMap<GetAccountGrantPaidToResponse, AccountGrantPaidToViewModel>();

            CreateMap<AccountGrantPaidToViewModel, SetAccountGrantPaidToRequest>();

            CreateMap<GetBankAccountDetailsRepresentativeResponse, BankAccountDetailsViewModel>();

            CreateMap<BankAccountDetailsViewModel, SetBankAccountDetailsRepresentativeRequest>();
        }
    }
}