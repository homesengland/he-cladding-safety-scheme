using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;

public class RightToManageEvidenceViewModelMapper : Profile
{
    public RightToManageEvidenceViewModelMapper()
    {
        CreateMap<GetRightToManageEvidenceResponse, RightToManageEvidenceViewModel>();
        CreateMap<FileResult, File>();

        CreateMap<RightToManageEvidenceViewModel, AddRightToManageEvidenceRequest>();

    }
}