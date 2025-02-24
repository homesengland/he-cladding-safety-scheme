using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class SelectViewModelMapper : Profile
{
    public SelectViewModelMapper()
    {
        CreateMap<GetSelectResponse, SelectViewModel>();
        CreateMap<GrantCertifyingOfficerCandidateResult, CandidateViewModel>();
        CreateMap<SelectViewModel, SetSelectRequest>();
    }
}
