using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportCompanyDetails;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProgressReportCompanyDetailsViewModelMapper : Profile
{
    public ProgressReportCompanyDetailsViewModelMapper()
    {
        CreateMap<GetProgressReportCompanyDetailsResponse, ProgressReportCompanyDetailsViewModel>();
    }
}