using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.Users;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement;

public class UsersViewModelMapper : Profile
{
    public UsersViewModelMapper()
    {
        CreateMap<UsersResponse, UsersViewModel>()
            .ForMember(dest => dest.OrganisationId, opt => opt.MapFrom(src => src.Organisation.Id))
            .ForMember(dest => dest.OrganisationName, opt => opt.MapFrom(src => src.Organisation.Name))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(x =>
                new UsersViewModel.CollaborationUser(
                    x.Id,
                    x.Name,
                    x.Email,
                    ((EApplicationRole)x.ApplicationRoleId).GetEnumDisplayName(),
                    ((ECollaborationUserStatus)x.UserStatusId).GetEnumDisplayName(),
                    x.Auth0UserId
                )
            )));
    }
}
