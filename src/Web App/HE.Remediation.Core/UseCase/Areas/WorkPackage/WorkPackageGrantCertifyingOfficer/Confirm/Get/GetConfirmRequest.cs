using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Get;

public class GetConfirmRequest : IRequest<GetConfirmResponse>
{
	private GetConfirmRequest()
	{
	}

	public static readonly GetConfirmRequest Request = new();
}
