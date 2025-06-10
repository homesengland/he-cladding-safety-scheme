﻿using MediatR;
using HE.Remediation.Core.Interface;
using Microsoft.Data.SqlClient;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember
{
    public class UpsertMemberHandler : IRequestHandler<UpsertMemberRequest, UpsertMemberResponse>
    {
        private readonly IDbConnectionWrapper _connection;

        public UpsertMemberHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async Task<UpsertMemberResponse> Handle(UpsertMemberRequest request, CancellationToken cancellationToken)
        {
            if(request.CollaborationUserId.HasValue)
            {
                return await UpdateCollaborationUser(request);
            } 
            else
            {
                return await InsertProvisionalCollaborationUser(request);
            } 
        }

        private async Task<UpsertMemberResponse> UpdateCollaborationUser(UpsertMemberRequest request)
        {
            await _connection.ExecuteAsync("UpdateCollaborationUser", new
            {
                CollaborationUserId = request.CollaborationUserId!.Value,
                request.FirstName,
                request.LastName,
                request.Email,
                ApplicationRoleId = (int)request.ApplicationRole,
                UserStatusId = (int)request.UserStatus.GetValueOrDefault()
            });

            return new UpsertMemberResponse() { CollaborationUserId = request.CollaborationUserId.Value };
        }

        private async Task<UpsertMemberResponse> InsertProvisionalCollaborationUser(UpsertMemberRequest request)
        {
            try
            {
                var collaborationUserId = await _connection.QuerySingleOrDefaultAsync<Guid>("InsertProvisionalCollaborationUser", new
                {
                    request.AdminUserId,
                    request.OrganisationId,
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    ApplicationRoleId = (int)request.ApplicationRole
                });

                return new UpsertMemberResponse() { CollaborationUserId = collaborationUserId };
            }
            catch (SqlException ex) when (ex.Message.Contains("Invalid admin user"))
            {
                throw new InvalidAdminOrganisationException();
            }
            catch (SqlException ex) when (ex.Message.Contains("User with given email already exists"))
            {
                throw new UserEmailExistsException();
            }
            catch (SqlException ex) when (ex.Message.Contains("Max number of admins exceeded"))
            {
                throw new MaximumAdminsException();
            }
        }
    }

    public class UpsertMemberRequest : IRequest<UpsertMemberResponse>
    {
        public Guid? CollaborationUserId { get; set; }
        public string AdminUserId { get; set; }
        public Guid OrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdminResponsibilityConfirmed { get; set; }
        public EApplicationRole ApplicationRole { get; set; }
        public ECollaborationUserStatus? UserStatus { get; set; }
    }

    public class UpsertMemberResponse
    {
        public Guid CollaborationUserId { get; set; }
    }

    public class InvalidAdminOrganisationException() : ApplicationException() { };
    public class UserEmailExistsException() : ApplicationException() { };
    public class MaximumAdminsException() : ApplicationException() { };
}
