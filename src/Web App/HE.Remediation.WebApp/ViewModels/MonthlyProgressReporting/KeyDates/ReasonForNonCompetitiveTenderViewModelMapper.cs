using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class ReasonForNonCompetitiveTenderViewModelMapper : Profile
{
    public ReasonForNonCompetitiveTenderViewModelMapper()
    {
        CreateMap<GetReasonForNonCompetitiveTenderResponse, ReasonForNonCompetitiveTenderViewModel>();
        CreateMap<ReasonForNonCompetitiveTenderViewModel, SetReasonForNonCompetitiveTenderRequest>();
    }
}
