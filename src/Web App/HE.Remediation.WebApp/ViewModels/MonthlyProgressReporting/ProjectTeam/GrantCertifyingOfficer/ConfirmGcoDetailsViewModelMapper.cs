using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class ConfirmGcoDetailsViewModelMapper : Profile
{
    public ConfirmGcoDetailsViewModelMapper()
    {
        CreateMap<GetGcoDetailsResponse, ConfirmGcoDetailsViewModel>();
        CreateMap<ConfirmGcoDetailsViewModel, SetGcoDetailsRequest>();
    }
}