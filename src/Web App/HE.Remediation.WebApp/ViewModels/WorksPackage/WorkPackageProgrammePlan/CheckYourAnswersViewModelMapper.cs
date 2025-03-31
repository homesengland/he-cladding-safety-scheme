using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetProjectPlanCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}