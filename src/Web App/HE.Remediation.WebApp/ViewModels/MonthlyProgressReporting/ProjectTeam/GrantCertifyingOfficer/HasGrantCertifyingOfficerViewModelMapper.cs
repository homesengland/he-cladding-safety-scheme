using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class HasGrantCertifyingOfficerViewModelMapper : Profile
{
    public HasGrantCertifyingOfficerViewModelMapper()
    {
        CreateMap<GetHasGrantCertifyingOfficerResponse, HasGrantCertifyingOfficerViewModel>();
        CreateMap<HasGrantCertifyingOfficerViewModel, SetHasGrantCertifyingOfficerRequest>();
    }
}