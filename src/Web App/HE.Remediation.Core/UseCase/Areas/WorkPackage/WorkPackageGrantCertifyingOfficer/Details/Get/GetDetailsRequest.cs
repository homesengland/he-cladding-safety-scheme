using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Get;

public class GetDetailsRequest : IRequest<GetDetailsResponse>
{
	private GetDetailsRequest()
	{
	}

	public static readonly GetDetailsRequest Request = new();
}
