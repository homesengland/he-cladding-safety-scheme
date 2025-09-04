using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.GetCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
        CreateMap<GetFundingRoutesCheckYourAnswersResult.PartyPursuedRole, CheckYourAnswersViewModel.PartyPursuedRoleViewModel>();
    }
}