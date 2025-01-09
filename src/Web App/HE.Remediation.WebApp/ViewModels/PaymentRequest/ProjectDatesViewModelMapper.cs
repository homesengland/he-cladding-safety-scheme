using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectDates;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetProjectDates;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ProjectDatesViewModelMapper : Profile
{
    public ProjectDatesViewModelMapper()
    {
        CreateMap<GetProjectDatesResponse, ProjectDatesViewModel>();
        CreateMap<ProjectDatesViewModel, SetProjectDatesRequest>();
    }
}
 