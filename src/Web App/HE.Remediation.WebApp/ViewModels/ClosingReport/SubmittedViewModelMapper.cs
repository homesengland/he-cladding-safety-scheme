using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitted;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class SubmittedViewModelMapper : Profile
{
    public SubmittedViewModelMapper()
    {
        CreateMap<GetSubmittedResponse, SubmittedViewModel>();
    }
}
