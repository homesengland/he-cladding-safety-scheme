using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ContractorContingency.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ContractorContingency.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class ContractorContingencyViewModelMapper : Profile
{
    public ContractorContingencyViewModelMapper()
    {
        CreateMap<GetContractorContingencyResponse, ContractorContingencyViewModel>();
        CreateMap<ContractorContingencyViewModel, SetContractorContingencyRequest>();
    }
}