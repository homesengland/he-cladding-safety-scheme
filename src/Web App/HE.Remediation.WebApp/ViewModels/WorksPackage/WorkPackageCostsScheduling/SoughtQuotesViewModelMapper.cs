using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SoughtQuotesViewModelMapper : Profile
{
    public SoughtQuotesViewModelMapper()
    {
        CreateMap<GetSoughtQuotesResponse, SoughtQuotesViewModel>();
        CreateMap<SoughtQuotesViewModel, SetSoughtQuotesRequest>();
    }
}
