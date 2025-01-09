using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractors;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class SubContractorsViewModelMapper : Profile
{
    public SubContractorsViewModelMapper()
    {
        CreateMap<GetSubContractorsResponse, SubContractorsViewModel>();
    }
}