using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunicationType;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunicationType;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderResponsibleForCommunicationTypeViewModelMapper : Profile
    {
        public LeaseHolderResponsibleForCommunicationTypeViewModelMapper()
        {
            CreateMap<LeaseHolderResponsibleForCommunicationTypeViewModel, SetResponsibleForCommunicationTypeRequest>();
            CreateMap<GetResponsibleForCommunicationTypeResponse, LeaseHolderResponsibleForCommunicationTypeViewModel>();
        }
    }
}
