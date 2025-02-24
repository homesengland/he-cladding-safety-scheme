using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.GetEvidence;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.SetEvidence;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class UploadEvidenceViewModelMapper : Profile
{
    public UploadEvidenceViewModelMapper()
    {
        CreateMap<FileResult, File>();
        CreateMap<GetEvidenceResponse, UploadEvidenceViewModel>()
            .ForMember(x => x.File, o => o.Ignore());
        
        CreateMap<UploadEvidenceViewModel, SetEvidenceRequest>();
    }
}