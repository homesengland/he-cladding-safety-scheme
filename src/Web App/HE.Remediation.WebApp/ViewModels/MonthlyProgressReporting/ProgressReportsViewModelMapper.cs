using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;

public class ProgressReportsViewModelMapper : Profile
{
    public ProgressReportsViewModelMapper()
    {
        CreateMap<GetProgressReportsResponse, ProgressReportsViewModel>();
        CreateMap<ProgressReportResult, ProgressReportSummaryViewModel>();
    }
}

