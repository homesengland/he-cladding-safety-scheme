using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.DutyOfCareDeed.Get;

public class GetDutyOfCareDeedRequest : IRequest<GetDutyOfCareDeedResponse>
{
    private GetDutyOfCareDeedRequest()
    {
    }

    public static GetDutyOfCareDeedRequest Request => new();
}
