using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetFinalCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class FinalCheckYourAnswersViewModelMapper : Profile
{
    public FinalCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetFinalCheckYourAnswersResponse, FinalCheckYourAnswersViewModel>();
        CreateMap<FileResult, ClosingReportUploadFile>();
    }
}
