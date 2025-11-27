using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class KeyDatesViewModelMapper : Profile
{
    public KeyDatesViewModelMapper()
    {
        CreateMap<GetKeyDatesResponse, KeyDatesViewModel>();
    }
}
