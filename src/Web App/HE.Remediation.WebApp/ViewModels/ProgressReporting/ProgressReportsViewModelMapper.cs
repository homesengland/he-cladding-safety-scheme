using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReports.GetProgressReports;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProgressReportsViewModelMapper : Profile
{
    public ProgressReportsViewModelMapper()
    {
        CreateMap<GetProgressReportsResponse, ProgressReportsViewModel>();
        CreateMap<ProgressReportResult, ProgressReportSummaryViewModel>();
    }
}

