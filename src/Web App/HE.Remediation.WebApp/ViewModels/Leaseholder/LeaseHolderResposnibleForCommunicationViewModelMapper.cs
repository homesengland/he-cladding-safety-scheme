using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunication;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderResposnibleForCommunicationViewModelMapper : Profile
    {
        public LeaseHolderResposnibleForCommunicationViewModelMapper()
        {
            CreateMap<LeaseHolderResponsibleForCommunicationViewModel, SetResponsibleForCommunicationRequest>();
            CreateMap<GetResponsibleForCommunicationResponse, LeaseHolderResponsibleForCommunicationViewModel>();
        }
    }
}
