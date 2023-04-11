using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class UploadEvidenceViewModelMapper : Profile
{
    public UploadEvidenceViewModelMapper()
    {
        CreateMap<GetUploadResponsibleEntitiesEvidenceResponse, UploadEvidenceViewModel>();
        CreateMap<UploadEvidenceViewModel, SetUploadResponsibleEntitiesEvidenceRequest>();
    }
}