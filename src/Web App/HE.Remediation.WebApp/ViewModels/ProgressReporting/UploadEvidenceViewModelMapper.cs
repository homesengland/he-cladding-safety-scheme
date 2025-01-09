using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.GetEvidence;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.SetEvidence;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class UploadEvidenceViewModelMapper : Profile
{
    public UploadEvidenceViewModelMapper()
    {
        CreateMap<GetEvidenceResponse, UploadEvidenceViewModel>()
            .ForMember(x => x.File, o => o.Ignore())
            .ForMember(x => x.AddedFile, o => o.MapFrom(f => f.File));

        CreateMap<UploadEvidenceViewModel, SetEvidenceRequest>();
    }
}