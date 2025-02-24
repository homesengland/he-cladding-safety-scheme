using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class NoQuotesViewModelMapper : Profile
{
    public NoQuotesViewModelMapper()
    {
        CreateMap<GetNoQuotesResponse, NoQuotesViewModel>();
        CreateMap<NoQuotesViewModel, SetNoQuotesRequest>();
    }
}
