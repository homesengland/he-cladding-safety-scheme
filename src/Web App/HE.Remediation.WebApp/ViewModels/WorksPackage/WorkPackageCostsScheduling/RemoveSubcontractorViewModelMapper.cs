using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class RemoveSubcontractorViewModelMapper : Profile
{
    public RemoveSubcontractorViewModelMapper()
    {
        CreateMap<GetRemoveSubcontractorResponse, RemoveSubcontractorViewModel>();
        CreateMap<RemoveSubcontractorViewModel, DeleteSubcontractorRequest>();
    }
}
