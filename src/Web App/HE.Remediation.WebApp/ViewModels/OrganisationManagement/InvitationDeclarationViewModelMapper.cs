using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InvitationDeclaration;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement;

public class InvitationDeclarationViewModelMapper : Profile
{
    public InvitationDeclarationViewModelMapper()
    {
        CreateMap<InvitationDeclarationViewModel, InvitationDeclarationRequest>();
    }
}
