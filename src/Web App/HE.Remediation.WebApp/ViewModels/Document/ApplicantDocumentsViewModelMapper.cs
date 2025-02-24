using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Document;

namespace HE.Remediation.WebApp.ViewModels.Document;

public class ApplicantDocumentsViewModelMapper : Profile
{
    public ApplicantDocumentsViewModelMapper()
    {
        CreateMap<GetApplicantDocumentsResponse, ApplicantDocumentsViewModel>();
        CreateMap<GetApplicantDocumentsResponse.DocumentFile, ApplicantDocumentsViewModel.ApplicantDocumentViewModel>();
    }
}