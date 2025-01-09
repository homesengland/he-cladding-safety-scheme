using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Get;

public class GetRequiresSubcontractorsRequest : IRequest<GetRequiresSubcontractorsResponse>
{
    private GetRequiresSubcontractorsRequest()
    {
    }

    public static GetRequiresSubcontractorsRequest Request => new();
}
