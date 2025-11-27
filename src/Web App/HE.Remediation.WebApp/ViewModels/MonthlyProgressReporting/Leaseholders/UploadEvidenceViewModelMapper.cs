using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

public class UploadEvidenceViewModelMapper : Profile
{
    public UploadEvidenceViewModelMapper()
    {
        CreateMap<GetUploadEvidenceResponse, UploadEvidenceViewModel>();
        CreateMap<UploadEvidenceViewModel, SetUploadEvidenceRequest>();
        CreateMap<GetUploadEvidenceResponse.FileResult, Shared.File>();
    }
}


