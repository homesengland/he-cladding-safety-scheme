using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetAccountGrantPaidTo;

namespace HE.Remediation.WebApp.ViewModels.BankAccount
{
    public class AccountGrantPaidToViewModelMapper : Profile
    {
        public AccountGrantPaidToViewModelMapper()
        {
            CreateMap<AccountGrantPaidToViewModel, SetAccountGrantPaidToRequest>();
        }
    }
}