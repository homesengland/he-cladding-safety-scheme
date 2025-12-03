using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class ContractorTenderViewModelMapper : Profile
{
    public ContractorTenderViewModelMapper()
    {
        CreateMap<GetContractorTenderResponse, ContractorTenderViewModel>();
        CreateMap<ContractorTenderViewModel, SetContractorTenderRequest>();
    }
}
