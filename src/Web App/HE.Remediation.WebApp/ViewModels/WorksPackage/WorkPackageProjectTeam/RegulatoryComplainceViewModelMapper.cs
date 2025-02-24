using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.RegulatoryCompliance;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class RegulatoryComplainceViewModelMapper : Profile
{
    public RegulatoryComplainceViewModelMapper()
    {
        CreateMap<GetRegulatoryComplianceResponse, RegulatoryComplianceViewModel>();
        CreateMap<RegulatoryComplianceViewModel, SetRegulatoryComplianceRequest>();
    }
}