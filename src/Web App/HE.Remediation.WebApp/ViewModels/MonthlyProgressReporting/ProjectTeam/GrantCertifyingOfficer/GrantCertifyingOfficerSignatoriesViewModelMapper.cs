using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GrantCertifyingOfficerSignatoriesViewModelMapper : Profile
{
    public GrantCertifyingOfficerSignatoriesViewModelMapper()
    {
        CreateMap<GetGrantCertifyingOfficerSignatoryResponse, GrantCertifyingOfficerSignatoriesViewModel>();
        CreateMap<GrantCertifyingOfficerSignatoriesViewModel, SetGrantCertifyingOfficerSignatoryRequest>();
    }   
}