using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetCheckYourUpdatedGcoMapper : Profile
{
    public GetCheckYourUpdatedGcoMapper()
    {
        CreateMap<GetCheckYourUpdatedGcoResponse, GetCheckYourUpdatedGcoViewModel>();
    }
}