using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCommunicationPartyDetails;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetCommunicationPartyDetails;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderCommunicationPartyDetailsViewModelMapper : Profile
    {
        public LeaseHolderCommunicationPartyDetailsViewModelMapper()
        {
            CreateMap<LeaseHolderCommunicationPartyDetailsViewModel, SetCommunicationPartyDetailsRequest>();
            CreateMap<GetCommunicationPartyDetailsResponse, LeaseHolderCommunicationPartyDetailsViewModel>();
        }
    }
}
