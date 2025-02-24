using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.SetReportDetails;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetReportDetailsViewModelMapper: Profile
{
    public GetReportDetailsViewModelMapper()
    {
        CreateMap<GetReportDetailsResponse, GetReportDetailsViewModel>();
        CreateMap<GetReportDetailsViewModel , SetReportDetailsRequest>();
    }
}
