using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SubcontractorViewModelMapper : Profile
{
    public SubcontractorViewModelMapper()
    {
        CreateMap<GetCostsSchedulingSubcontractorResponse, SubcontractorViewModel>();
        CreateMap<SubcontractorViewModel, SetCostsSchedulingSubcontractorRequest>();
    }
}