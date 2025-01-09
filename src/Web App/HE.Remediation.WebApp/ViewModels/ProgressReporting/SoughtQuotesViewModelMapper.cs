using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.GetSoughtQuotes;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.SetSoughtQuotes;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class SoughtQuotesViewModelMapper : Profile
{
    public SoughtQuotesViewModelMapper()
    {
        CreateMap<GetSoughtQuotesResponse, SoughtQuotesViewModel>();
        CreateMap<SoughtQuotesViewModel, SetSoughtQuotesRequest>();
    }
}
