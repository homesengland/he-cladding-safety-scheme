using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates;

namespace HE.Remediation.WebApp.ViewModels.Shared;

public class KeyDatesChangedViewModelMapper : Profile
{
    public KeyDatesChangedViewModelMapper()
    {
        CreateMap<GetProgressReportKeyDatesChangeTypesResult, KeyDatesChangedViewModel.ChangeTypeViewModel>();
    }
}