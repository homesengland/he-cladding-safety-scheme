using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class CheckYourAnswersViewModelMapper : Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();

            CreateMap<Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers.LeaseHolderEvidenceFile, LeaseHolderEvidenceFile>();
        }
    }
}
