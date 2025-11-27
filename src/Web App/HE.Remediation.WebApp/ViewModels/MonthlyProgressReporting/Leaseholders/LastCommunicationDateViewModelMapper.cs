using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

public class LastCommunicationDateViewModelMapper : Profile
{
    public LastCommunicationDateViewModelMapper()
    {
        CreateMap<GetLastCommunicationDateResponse, LastCommunicationDateViewModel>();
        CreateMap<LastCommunicationDateViewModel, SetLastCommunicationDateRequest>();
    }
}
