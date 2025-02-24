
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.StartInformation.Set;

public class SetStartInformationRequest : IRequest<Unit>
{
    private SetStartInformationRequest()
    {
    }

    public static readonly SetStartInformationRequest Request = new();
}
