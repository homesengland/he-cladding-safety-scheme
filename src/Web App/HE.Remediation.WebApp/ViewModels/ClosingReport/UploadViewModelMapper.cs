using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.AddFile;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetUpload;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class UploadViewModelMapper : Profile
{
    public UploadViewModelMapper()
    {
        CreateMap<GetUploadResponse, UploadViewModel>();
        CreateMap<UploadViewModel, AddFileRequest>();
        //CreateMap<UploadViewModel, SetUploadRequest>();
    }
}