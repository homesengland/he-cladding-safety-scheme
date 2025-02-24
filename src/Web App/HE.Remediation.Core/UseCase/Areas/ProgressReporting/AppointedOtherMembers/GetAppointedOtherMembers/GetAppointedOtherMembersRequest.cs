using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.GetAppointedOtherMembers;

public class GetAppointedOtherMembersRequest : IRequest<GetAppointedOtherMembersResponse>
{
    private GetAppointedOtherMembersRequest()
    {
    }

    public static readonly GetAppointedOtherMembersRequest Request = new();
}
