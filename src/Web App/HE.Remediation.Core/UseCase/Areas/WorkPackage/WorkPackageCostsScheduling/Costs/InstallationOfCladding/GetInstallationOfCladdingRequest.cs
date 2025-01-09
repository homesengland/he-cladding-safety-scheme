using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.InstallationOfCladding;

public class GetInstallationOfCladdingRequest : IRequest<GetInstallationOfCladdingResponse>
{
    private GetInstallationOfCladdingRequest()
    {
    }

    public static readonly GetInstallationOfCladdingRequest Request = new();
}