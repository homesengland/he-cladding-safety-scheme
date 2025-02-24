using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportDetails;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProgressReportDetailsViewModelMapper : Profile
{
    public ProgressReportDetailsViewModelMapper()
    {
        CreateMap<GetProgressReportDetailsResponse, ProgressReportDetailsViewModel>();
        CreateMap<GetProgressReportDetailsResponse.TeamMember, ProgressReportDetailsViewModel.TeamMember>();
    }
}