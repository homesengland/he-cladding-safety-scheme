using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.GetConfirmToProceed;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.SetConfirmToProceed;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.ConfirmToProceed
{
    public class ConfirmToProceedViewModelMapper : Profile
    {
        public ConfirmToProceedViewModelMapper()
        {
            CreateMap<GetWorkPackageConfirmToProceedResponse, ConfirmToProceedViewModel>();
            CreateMap<ConfirmToProceedViewModel, SetWorkPackageConfirmToProceedRequest>();
        }
    }
}
