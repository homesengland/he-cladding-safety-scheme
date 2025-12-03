using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.Remediation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class RemediationDatesChangedViewModelMapper : Profile
{
    public RemediationDatesChangedViewModelMapper()
    {
        CreateMap<GetRemediationDatesChangedResponse, RemediationDatesChangedViewModel>();
        CreateMap<RemediationDatesChangedViewModel, SetRemediationDatesChangedRequest>();
    }
}