using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Dashboard.GetProfile;

public class GetProfileRequest : IRequest<GetProfileResponse>
{
    public static readonly GetProfileRequest Request = new();
}
